using MvcApiFarm.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcApiFarm.Controllers;

public class FornecedoresController : Controller
{
    private readonly ApplicationDbContext context;

    public FornecedoresController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var listaFornecedores = context.Fornecedores.ToList();
        return View(listaFornecedores);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Fornecedor fornecedor)
    {
        if (!ValidarCnpj(fornecedor.Cnpj))
        {
            ModelState.AddModelError("Cnpj", "CNPJ inválido.");
            return View(fornecedor);
        }

        context.Fornecedores.Add(fornecedor);
        context.SaveChanges();

        return RedirectToAction("Index");
    }


    public IActionResult Editar(int id)
    {
        var fornecedor = context.Fornecedores.Find(id);
        if (fornecedor == null) return NotFound();
        return View(fornecedor);
    }

    [HttpPost]
    public IActionResult Editar(Fornecedor fornecedor)
    {
        var fornecedorExistente = context.Fornecedores.Find(fornecedor.Id);
        if (fornecedorExistente == null) return NotFound();
        if (!ValidarCnpj(fornecedor.Cnpj))
        {
            ModelState.AddModelError("Cnpj", "CNPJ inválido.");
            return View(fornecedor);
        }

        fornecedorExistente.Nome = fornecedor.Nome;
        fornecedorExistente.Cnpj = fornecedor.Cnpj;
        fornecedorExistente.Representante = fornecedor.Representante;
        fornecedorExistente.Telefone = fornecedor.Telefone;
        context.Fornecedores.Update(fornecedorExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Remover(int id)
    {
        var fornecedor = context.Fornecedores.Find(id);
        if (fornecedor == null) return NotFound();
        return View(fornecedor);
    }

    [HttpPost]
    public IActionResult Remover(Fornecedor fornecedor)
    {
        var fornecedorExistente = context.Fornecedores.Find(fornecedor.Id);
        if (fornecedorExistente == null) return NotFound();

        context.Fornecedores.Remove(fornecedorExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Detalhes(int id)
    {
        var fornecedor = context.Fornecedores.Find(id);
        if (fornecedor == null) return NotFound();
        return View(fornecedor);
    }

    private bool ValidarCnpj(string cnpj)
    {
        // Remove caracteres não numéricos
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();

        // Verifica se o CNPJ tem 14 dígitos
        if (cnpj.Length != 14 || !long.TryParse(cnpj, out _)) return false;

        // Cálculo do primeiro dígito verificador
        int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var soma1 = 0;

        for (var i = 0; i < 12; i++) soma1 += int.Parse(cnpj[i].ToString()) * multiplicadores1[i];

        var resto1 = soma1 % 11;
        var primeiroDigitoVerificador = resto1 < 2 ? 0 : 11 - resto1;

        // Verifica o primeiro dígito verificador
        if (primeiroDigitoVerificador != int.Parse(cnpj[12].ToString())) return false;

        // Cálculo do segundo dígito verificador
        int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var soma2 = 0;

        for (var i = 0; i < 13; i++) soma2 += int.Parse(cnpj[i].ToString()) * multiplicadores2[i];

        var resto2 = soma2 % 11;
        var segundoDigitoVerificador = resto2 < 2 ? 0 : 11 - resto2;

        // Verifica o segundo dígito verificador
        if (segundoDigitoVerificador != int.Parse(cnpj[13].ToString())) return false;

        return true; // Retorna verdadeiro se o CNPJ for válido
    }
}