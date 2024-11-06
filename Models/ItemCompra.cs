namespace MvcApiFarm.Models;

public class ItemCompra
{
    public int Id { get; set; }

    public int CompraId { get; set; }
    public Compra Compra { get; set; }

    public int RecursoId { get; set; }
    public Recurso Recurso { get; set; }

    public int Quantidade { get; set; }
    public DateTime PrevisaoEntrega { get; set; }
    public decimal Preco { get; set; }
    public decimal SubTotal => Quantidade * Preco;
}