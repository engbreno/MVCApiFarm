namespace MvcApiFarm.Models;

public class ItemNFS
{
    public int Id { get; set; }
    public int ItemCompraId { get; set; }
    public ItemCompra ItemCompra { get; set; }
    public int NFSaidaId { get; set; }
    public NFSaida NFSaida { get; set; }
}