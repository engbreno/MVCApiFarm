namespace MvcApiFarm.Models;

public class Fornecedor
{
    public int Id { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Representante { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}