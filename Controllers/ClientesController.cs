using MvcApiFarm.Models;
using Microsoft.AspNetCore.Mvc;

// Adicione esta linha para usar BCrypt

namespace MvcApiFarm.Controllers;

public class ClientesController : Controller
{
    private readonly ApplicationDbContext context;

    public ClientesController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var listaClientes = context.Clientes.ToList();
        return View(listaClientes);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Cliente cliente, string confirmarSenha)
    {
        if (cliente.Senha != confirmarSenha)
        {
            ModelState.AddModelError("Senha", "As senhas não coincidem.");
            return View(cliente); // Retorna à view com a mensagem de erro
        }

        // Criptografa a senha antes de salvar
        cliente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);
        context.Clientes.Add(cliente);
        context.SaveChanges();

        return RedirectToAction("Index");
    }


    public IActionResult Editar(int id)
    {
        var cliente = context.Clientes.Find(id);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

    [HttpPost]
    public IActionResult Editar(Cliente cliente)
    {
        var clienteExistente = context.Clientes.Find(cliente.Id);
        if (clienteExistente == null) return NotFound();

        // Somente atualiza a senha se uma nova for fornecida
        if (!string.IsNullOrEmpty(cliente.Senha))
            clienteExistente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);
        clienteExistente.Nome = cliente.Nome;
        clienteExistente.Email = cliente.Email;
        clienteExistente.Data_Nascimento = cliente.Data_Nascimento;
        clienteExistente.Endereco = cliente.Endereco;
        clienteExistente.Telefone = cliente.Telefone;
        context.Clientes.Update(clienteExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Remover(int id)
    {
        var cliente = context.Clientes.Find(id);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

    [HttpPost]
    public IActionResult Remover(Cliente cliente)
    {
        var clienteExistente = context.Clientes.Find(cliente.Id);
        if (clienteExistente == null) return NotFound();

        context.Clientes.Remove(clienteExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Detalhes(int id)
    {
        var cliente = context.Clientes.Find(id);
        if (cliente == null) return NotFound();
        return View(cliente);
    }
}