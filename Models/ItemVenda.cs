namespace MvcApiFarm.Models;

public class ItemVenda
{
    public int Id { get; set; }

    public int VendaId { get; set; }
    public required Venda Venda { get; set; }

    public int RecursoId { get; set; }
    public required Recurso Recurso { get; set; }

    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public decimal SubTotal => Quantidade * Preco;
}