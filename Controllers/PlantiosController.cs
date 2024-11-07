using MvcApiFarm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MvcApiFarm.Controllers;

public class PlantiosController : Controller
{
    private readonly ApplicationDbContext context;

    public PlantiosController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var listaPlantios = context.Plantios
            .Include(f => f.AreaPlantio)
            .ToList();
        return View(listaPlantios);
    }

    public IActionResult Criar()
    {
        ListaRecursos();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(Plantio plantio)
    {
        ModelState.Clear();
        Console.WriteLine($"Plantio: {JsonConvert.SerializeObject(plantio)}");

        if (ModelState.IsValid)
        {
            Console.WriteLine("Plantio: Válido");
            try
            {
                // Adiciona o plantio
                context.Plantios.Add(plantio);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao salvar o plantio: {ex.Message}");
            }
        }
        else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                Console.WriteLine($"Erro de validação: {error.ErrorMessage}");
        }

        ListaRecursos();
        return View(plantio);
    }

    public IActionResult Editar(int id)
    {
        var plantio = context.Plantios
            .Include(p => p.ItensPlantio)
            .ThenInclude(ip => ip.Recurso)
            .FirstOrDefault(p => p.Id == id);

        if (plantio == null) return NotFound();

        ListaRecursos();
        return View(plantio);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Plantio plantio)
    {
        Console.WriteLine($"Plantio: {JsonConvert.SerializeObject(plantio)}");

        if (ModelState.IsValid)
            try
            {
                // Busca o plantio existente no banco de dados
                var plantioExistente = await context.Plantios
                    .Include(p => p.ItensPlantio)
                    .ThenInclude(ip => ip.Recurso)
                    .FirstOrDefaultAsync(p => p.Id == plantio.Id);

                if (plantioExistente == null) return NotFound();

                // Atualiza os campos do plantio existente
                plantioExistente.DataPlantio = plantio.DataPlantio;
                plantioExistente.AreaPlantioId = plantio.AreaPlantioId;

                // Identifica e remove os itens antigos que não estão mais presentes no plantio atualizado
                var itensParaRemover = plantioExistente.ItensPlantio
                    .Where(itemExistente => !plantio.ItensPlantio.Any(i => i.Id == itemExistente.Id))
                    .ToList();

                foreach (var item in itensParaRemover) context.ItemsPlantio.Remove(item);

                // Atualiza itens existentes e adiciona novos itens
                foreach (var item in plantio.ItensPlantio)
                    if (item.Id == 0)
                    {
                        // Adiciona novo item de plantio
                        plantioExistente.ItensPlantio.Add(new ItemPlantio
                        {
                            RecursoId = item.RecursoId,
                            Quantidade = item.Quantidade,
                            PlantioId = plantioExistente.Id
                        });
                    }
                    else
                    {
                        // Atualiza o item existente
                        var itemExistente = plantioExistente.ItensPlantio
                            .FirstOrDefault(i => i.Id == item.Id);

                        if (itemExistente != null)
                        {
                            itemExistente.RecursoId = item.RecursoId;
                            itemExistente.Quantidade = item.Quantidade;
                        }
                    }

                await context.SaveChangesAsync();

                ListaRecursos();
                plantio.ItensPlantio = await context.ItemsPlantio
                    .Where(ip => ip.PlantioId == plantio.Id)
                    .Include(ip => ip.Recurso)
                    .ToListAsync();

                return View(plantio);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao atualizar o plantio: {ex.Message}");
            }
        else
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                Console.WriteLine($"Erro de validação: {error.ErrorMessage}");

        return RedirectToAction(nameof(Index));
    }


    public IActionResult Remover(int id)
    {
        var plantio = context.Plantios
            .Include(f => f.AreaPlantio)
            .FirstOrDefault(f => f.Id == id);

        if (plantio == null) return NotFound();

        return View(plantio);
    }

    [HttpPost]
    public IActionResult Remover(Plantio plantio)
    {
        var plantioExistente = context.Plantios.Find(plantio.Id);
        if (plantioExistente == null) return NotFound();

        context.Plantios.Remove(plantioExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Detalhes(int id)
    {
        var plantio = context.Plantios
            .Include(f => f.AreaPlantio)
            .Include(p => p.ItensPlantio)
            .FirstOrDefault(f => f.Id == id);

        if (plantio == null) return NotFound();

        return View(plantio);
    }

    private void ListaRecursos()
    {
        ViewBag.Recursos = new SelectList(context.Recursos, "Id", "Nome");
        ViewBag.RecursosMaquinario = context.Recursos
            .Where(r => r.Tipo == "Maquinário" && r.Status == "Disponível")
            .Select(r => new {
                Id = r.Id,
                Nome = r.Nome,
                NumeroSerie = r.NumeroSerie
            }).ToList();
        ViewBag.RecursosInsumos = new SelectList(context.Recursos.Where(r => r.Tipo == "Insumo"), "Id", "Nome");
        ViewBag.RecursosProdutos = new SelectList(context.Recursos.Where(r => r.Tipo == "Produto"), "Id", "Nome");
        ViewBag.AreasPlantio = new SelectList(context.AreasPlantio, "Id", "Nome");
    }

    [HttpGet]
    public async Task<IActionResult> GetPrecoRecurso(int idRecurso)
    {
        var recurso = await context.Recursos.FindAsync(idRecurso);
        if (recurso == null) return NotFound();
        return Json(new { preco = recurso.Preco, quantidadeDisponivel = recurso.Quantidade });
    }

    [HttpGet]
    public async Task<IActionResult> GetSerieRecurso(int idRecurso)
    {
        var recurso = await context.Recursos.FindAsync(idRecurso);
        if (recurso == null) return NotFound();
        return Json(recurso.NumeroSerie);
    }
}