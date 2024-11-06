using MvcApiFarm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MvcApiFarm.Controllers;

public class AreasPlantioController : Controller
{
    private readonly ApplicationDbContext context;

    public AreasPlantioController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var listaAreas = context.AreasPlantio
            .Include(f => f.Fazenda)
            .ToList();
        return View(listaAreas);
    }

    public IActionResult Criar()
    {
        SetListaTiposStatus();
        ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
        return View();
    }

    [HttpPost]
    public IActionResult Criar(AreaPlantio areaPlantio)
    {
        SetListaTiposStatus();
        ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
        context.AreasPlantio.Add(areaPlantio);
        context.SaveChanges();

        return RedirectToAction("Index");
    }


    public IActionResult Editar(int id)
    {
        var areaPlantio = context.AreasPlantio.Find(id);
        if (areaPlantio == null) return NotFound();
        SetListaTiposStatus();
        ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
        return View(areaPlantio);
    }

    [HttpPost]
    public IActionResult Editar(AreaPlantio areaPlantio)
    {
        var areaPlantioExistente = context.AreasPlantio.Find(areaPlantio.Id);
        if (areaPlantioExistente == null) return NotFound();
        SetListaTiposStatus();
        ViewBag.Fazendas = new SelectList(context.Fazendas, "Id", "Nome");
        areaPlantioExistente.Nome = areaPlantio.Nome;
        areaPlantioExistente.FazendaId = areaPlantio.FazendaId;
        areaPlantioExistente.Hectares = areaPlantio.Hectares;
        areaPlantioExistente.Localizacao = areaPlantio.Localizacao;
        areaPlantioExistente.Status = areaPlantio.Status;
        areaPlantioExistente.Descricao = areaPlantio.Descricao;
        context.AreasPlantio.Update(areaPlantioExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Remover(int id)
    {
        var areaPlantio = context.AreasPlantio
            .Include(f => f.Fazenda) // Inclui a fazenda associada
            .FirstOrDefault(f => f.Id == id);
        if (areaPlantio == null) return NotFound();
        return View(areaPlantio);
    }

    [HttpPost]
    public IActionResult Remover(AreaPlantio areaPlantio)
    {
        var areaPlantioExistente = context.AreasPlantio.Find(areaPlantio.Id);
        if (areaPlantioExistente == null) return NotFound();

        context.AreasPlantio.Remove(areaPlantioExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Detalhes(int id)
    {
        var areaPlantio = context.AreasPlantio
            .Include(f => f.Fazenda) // Inclui a fazenda associada
            .FirstOrDefault(f => f.Id == id);
        if (areaPlantio == null) return NotFound();
        return View(areaPlantio);
    }

    private void SetListaTiposStatus()
    {
        var ListaTiposStatus = new List<string>
        {
            "Em plantio",
            "Pronta para colheita",
            "Disponível"
        };
        ViewBag.TiposStatus = new SelectList(ListaTiposStatus);
    }
}