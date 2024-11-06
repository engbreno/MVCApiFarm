using MvcApiFarm.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcApiFarm.Controllers;

public class FazendasController : Controller
{
    private readonly ApplicationDbContext context;

    public FazendasController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var listaFazendas = context.Fazendas.ToList();
        return View(listaFazendas);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Fazenda fazenda)
    {
        context.Fazendas.Add(fazenda);
        context.SaveChanges();

        return RedirectToAction("Index");
    }


    public IActionResult Editar(int id)
    {
        var fazenda = context.Fazendas.Find(id);
        if (fazenda == null) return NotFound();
        return View(fazenda);
    }

    [HttpPost]
    public IActionResult Editar(Fazenda fazenda)
    {
        var fazendaExistente = context.Fazendas.Find(fazenda.Id);
        if (fazendaExistente == null) return NotFound();

        fazendaExistente.Nome = fazenda.Nome;
        fazendaExistente.Hectares = fazenda.Hectares;
        fazendaExistente.Email = fazenda.Email;
        fazendaExistente.Endereco = fazenda.Endereco;
        fazendaExistente.Telefone = fazenda.Telefone;
        context.Fazendas.Update(fazendaExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Remover(int id)
    {
        var fazenda = context.Fazendas.Find(id);
        if (fazenda == null) return NotFound();
        return View(fazenda);
    }

    [HttpPost]
    public IActionResult Remover(Fazenda fazenda)
    {
        var fazendaExistente = context.Fazendas.Find(fazenda.Id);
        if (fazendaExistente == null) return NotFound();

        context.Fazendas.Remove(fazendaExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Detalhes(int id)
    {
        var fazenda = context.Fazendas.Find(id);
        if (fazenda == null) return NotFound();
        return View(fazenda);
    }
}