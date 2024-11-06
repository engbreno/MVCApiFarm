using MvcApiFarm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MvcApiFarm.Controllers;

public class VendasController(ApplicationDbContext context) : Controller
{
    public IActionResult Index()
    {
        var listaVendas = context.Vendas
            .Include(v => v.Cliente)
            .Include(v => v.ItensVenda)
            .ThenInclude(i => i.Recurso) // Inclui os produtos nos itens
            .ToList();
        return View(listaVendas);
    }

    // GET: Criação de Venda
    public IActionResult Criar()
    {
        AddBags();
        return View();
    }

    [HttpGet]
    public JsonResult GetPrecoRecurso(int idRecurso)
    {
        var recurso = context.Recursos.FirstOrDefault(p => p.Id == idRecurso);
        if (recurso != null) return Json(recurso.Preco);
        return Json(0);
    }

    // POST: Salvar a Venda e seus itens
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(Venda venda, List<ItemVenda> itensVenda)
    {
        context.Add(venda);
        await context.SaveChangesAsync();
        venda.ItensVenda = itensVenda;
        foreach (var item in itensVenda)
        {
            item.VendaId = venda.Id;
            var recurso = context.Recursos.Find(item.RecursoId);
            if (recurso != null) item.Preco = recurso.Preco;
            context.ItemsVenda.Add(item); // Adiciona o item apenas uma vez
        }


        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Vendas/Editar/5
    public IActionResult Editar(int id)
    {
        var venda = context.Vendas
            .Include(v => v.ItensVenda)
            .FirstOrDefault(v => v.Id == id);

        if (venda == null) return NotFound();

        AddBags();

        return View(venda);
    }

    // POST: Vendas/Editar/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, Venda venda, List<ItemVenda> itensVenda)
    {
        if (id != venda.Id) return BadRequest();

        if (ModelState.IsValid)
            try
            {
                context.Update(venda);

                // Atualizar os itens da venda
                var itensExistentes = context.ItemsVenda.Where(iv => iv.VendaId == id).ToList();
                context.ItemsVenda.RemoveRange(itensExistentes);

                foreach (var item in itensVenda)
                {
                    item.VendaId = venda.Id;
                    item.Preco = context.Recursos.Find(item.RecursoId).Preco;
                    context.ItemsVenda.Add(item);
                }

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Vendas.Any(v => v.Id == venda.Id))
                    return NotFound();
                throw;
            }

        // Recarregar os dados para os dropdowns em caso de erro de validação
        AddBags();

        return View(venda);
    }

    public IActionResult Detalhes(int id)
    {
        var venda = context.Vendas
            .Include(v => v.Cliente)
            .Include(v => v.ItensVenda)
            .ThenInclude(i => i.Recurso) // Inclui os produtos nos itens
            .FirstOrDefault(v => v.Id == id);
        if (venda == null) return NotFound();
        return View(venda);
    }

    public IActionResult Remover(int id)
    {
        var venda = context.Vendas
            .Include(v => v.Cliente)
            .Include(v => v.ItensVenda)
            .ThenInclude(i => i.Recurso) // Inclui os produtos nos itens
            .FirstOrDefault(v => v.Id == id);
        if (venda == null) return NotFound();
        return View(venda);
    }

    [HttpPost]
    public IActionResult Remover(Venda venda)
    {
        var vendaExistente = context.Vendas.Find(venda.Id);
        if (vendaExistente == null) return NotFound();

        context.Vendas.Remove(vendaExistente);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    private void AddBags()
    {
        var ListaFormaPagamento = new List<string>
        {
            "Cartão de Débito",
            "Cartão de Crédito",
            "Boleto",
            "PIX"
        };
        ViewBag.FormaPagamento = new SelectList(ListaFormaPagamento);
        var ListaFormaEntrega = new List<string>
        {
            "Entrega",
            "Retirada"
        };
        ViewBag.FormaEntrega = new SelectList(ListaFormaEntrega);
        var ListaStatus = new List<string>
        {
            "Aberta",
            "Fechada"
        };
        ViewBag.Status = new SelectList(ListaStatus);
        ViewBag.Clientes = new SelectList(context.Clientes, "Id", "Nome");
        ViewBag.Recursos = new SelectList(context.Recursos.Where(r => r.Tipo == "Produto"), "Id", "Nome");
    }
}