using MvcApiFarm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcApiFarm.Controllers;

public class RecursosController(ApplicationDbContext context) : Controller
{
    public IActionResult Index()
    {
        var listaRecursos = context.Recursos.ToList();
        return View(listaRecursos);
    }


    public IActionResult Criar()
    {
        SetListaStatusRecurso();
        SetListaTiposRecursos();
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Recurso Recurso)
    {
        SetListaStatusRecurso();
        SetListaTiposRecursos();
        context.Recursos.Add(Recurso);
        context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Editar(int id)
    {
        var recurso = context.Recursos.Find(id);
        if (recurso == null) return NotFound();
        SetListaStatusRecurso();
        SetListaTiposRecursos();
        ViewBag.IsMaquinario = recurso.Tipo == "Maquinário";
        return View(recurso);
    }

    [HttpPost]
    public IActionResult Editar(Recurso recurso)
    {
        var recursoExistente = context.Recursos.Find(recurso.Id);
        if (recursoExistente == null) return NotFound();
        SetListaStatusRecurso();
        SetListaTiposRecursos();
        recursoExistente.Nome = recurso.Nome;
        recursoExistente.Preco = recurso.Preco;
        recursoExistente.UnidadeMedida = recurso.UnidadeMedida;
        recursoExistente.Quantidade = recurso.Quantidade;
        recursoExistente.Tipo = recurso.Tipo;
        recursoExistente.NumeroSerie = recurso.NumeroSerie;
        recursoExistente.DataAquisicao = recurso.DataAquisicao;
        recursoExistente.Status = recurso.Status;
        context.Recursos.Update(recursoExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    private void SetListaTiposRecursos()
    {
        var ListaTiposRecursos = new List<string>
        {
            "Matéria-Prima",
            "Produto",
            "Maquinário"
        };
        ViewBag.TiposRecursos = new SelectList(ListaTiposRecursos);
    }

    public IActionResult Remover(int id)
    {
        var recurso = context.Recursos.Find(id);
        if (recurso == null) return NotFound();
        return View(recurso);
    }

    [HttpPost]
    public IActionResult Remover(Recurso recurso)
    {
        var recursoExistente = context.Recursos.Find(recurso.Id);
        if (recursoExistente == null) return NotFound();

        context.Recursos.Remove(recursoExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Detalhes(int id)
    {
        var recurso = context.Recursos.Find(id);
        if (recurso == null) return NotFound();
        return View(recurso);
    }

    private void SetListaStatusRecurso()
    {
        var ListaStatusRecursos = new List<string>
        {
            "Ocupado",
            "Disponível",
            "Em manutenção"
        };
        ViewBag.StatusRecursos = new SelectList(ListaStatusRecursos);
    }
}