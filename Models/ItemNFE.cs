namespace MvcApiFarm.Models;

public class ItemNFE
{
    public int Id { get; set; }
    public int ItemVendaId { get; set; }
    public ItemVenda ItemVenda { get; set; }
    public int NFEntradaId { get; set; }
    public NFEntrada NFEntrada { get; set; }
}