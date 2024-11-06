namespace MvcApiFarm.Models;

public class Compra
{
    public int Id { get; set; }
    public int FornecedorId { get; set; }
    public required Fornecedor Fornecedor { get; set; }
    public int FuncionarioId { get; set; }
    public required Funcionario Funcionario { get; set; }

    public DateTime DataVenda { get; set; }
    public string Status { get; set; }

    public string FormaPagamento { get; set; }

    public string FormaEntrega { get; set; }

    public decimal Total => ItensCompra?.Sum(item => item.SubTotal) ?? 0;

    public ICollection<ItemCompra> ItensCompra { get; set; }
}