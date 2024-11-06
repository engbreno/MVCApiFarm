using MvcApiFarm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// Adicione esta linha para usar BCrypt

namespace MvcApiFarm.Controllers;

public class FuncionariosController : Controller
{
    private readonly ApplicationDbContext context;

    public FuncionariosController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var listaFuncionarios = context.Funcionarios
            .Include(f => f.Fazenda)
            .ToList();
        return View(listaFuncionarios);
    }

    public IActionResult Criar()
    {
        ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Funcionario funcionario, string confirmarSenha)
    {
        if (!ValidarCpf(funcionario.Cpf))
        {
            ModelState.AddModelError("Cpf", "CPF inválido.");
            ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
            return View(funcionario);
        }

        if (funcionario.Senha != confirmarSenha)
        {
            ModelState.AddModelError("Senha", "As senhas não coincidem.");
            ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
            return View(funcionario); // Retorna à view com a mensagem de erro
        }

        // Criptografa a senha antes de salvar
        funcionario.Senha = BCrypt.Net.BCrypt.HashPassword(funcionario.Senha);
        context.Funcionarios.Add(funcionario);
        context.SaveChanges();
        return RedirectToAction("Index");
    }


    public IActionResult Editar(int id)
    {
        var funcionario = context.Funcionarios.Find(id);
        if (funcionario == null) return NotFound();
        ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
        return View(funcionario);
    }

    [HttpPost]
    public IActionResult Editar(Funcionario funcionario)
    {
        var funcionarioExistente = context.Funcionarios.Find(funcionario.Id);
        if (funcionarioExistente == null) return NotFound();
        if (!ValidarCpf(funcionario.Cpf))
        {
            ModelState.AddModelError("Cpf", "CPF inválido.");
            ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
            return View(funcionario);
        }

        // Somente atualiza a senha se uma nova for fornecida
        if (!string.IsNullOrEmpty(funcionario.Senha))
            funcionarioExistente.Senha = BCrypt.Net.BCrypt.HashPassword(funcionario.Senha);
        funcionarioExistente.Nome = funcionario.Nome;
        funcionarioExistente.FazendaId = funcionario.FazendaId;
        funcionarioExistente.Cpf = funcionario.Cpf;
        funcionarioExistente.Telefone = funcionario.Telefone;
        funcionarioExistente.Funcao = funcionario.Funcao;
        funcionarioExistente.Salario = funcionario.Salario;
        context.Funcionarios.Update(funcionarioExistente);
        context.SaveChanges();
        ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
        return RedirectToAction("Index");
    }

    public IActionResult Remover(int id)
    {
        var funcionario = context.Funcionarios
            .Include(f => f.Fazenda) // Inclui a fazenda associada
            .FirstOrDefault(f => f.Id == id);
        if (funcionario == null) return NotFound();
        return View(funcionario);
    }

    [HttpPost]
    public IActionResult Remover(Funcionario funcionario)
    {
        var funcionarioExistente = context.Funcionarios.Find(funcionario.Id);
        if (funcionarioExistente == null) return NotFound();

        context.Funcionarios.Remove(funcionarioExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Detalhes(int id)
    {
        var funcionario = context.Funcionarios
            .Include(f => f.Fazenda) // Inclui a fazenda associada
            .FirstOrDefault(f => f.Id == id);
        if (funcionario == null) return NotFound();
        return View(funcionario);
    }

    private bool ValidarCpf(string cpf)
    {
        // Remove caracteres não numéricos
        cpf = cpf.Replace(".", "").Replace("-", "").Trim();

        // Verifica se o CPF tem 11 dígitos
        if (cpf.Length != 11 || !long.TryParse(cpf, out _)) return false;
        // Cálculo do primeiro dígito verificador
        var soma = 0;
        for (var i = 0; i < 9; i++) soma += int.Parse(cpf[i].ToString()) * (10 - i);
        var resto = soma % 11;
        var primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto;

        // Verifica o primeiro dígito verificador
        if (primeiroDigitoVerificador != int.Parse(cpf[9].ToString())) return false;

        // Cálculo do segundo dígito verificador
        soma = 0;
        for (var i = 0; i < 10; i++) soma += int.Parse(cpf[i].ToString()) * (11 - i);
        resto = soma % 11;
        var segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

        // Verifica o segundo dígito verificador
        if (segundoDigitoVerificador != int.Parse(cpf[10].ToString())) return false;

        return true; // Retorna verdadeiro se o CPF for válido
    }
}