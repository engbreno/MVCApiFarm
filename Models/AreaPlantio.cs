namespace MvcApiFarm.Models;

public class AreaPlantio
{
    public int Id { get; set; }
    public int FazendaId { get; set; }
    public required Fazenda Fazenda { get; set; }
    public string Nome { get; set; }
    public decimal Hectares { get; set; }
    public string Localizacao { get; set; }
    public string Status { get; set; }
    public string Descricao { get; set; }
}