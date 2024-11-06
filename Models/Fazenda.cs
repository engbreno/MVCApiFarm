namespace MvcApiFarm.Models;

public class Fazenda
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Hectares { get; set; }
    public string Endereco { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
}