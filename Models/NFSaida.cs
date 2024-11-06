namespace MvcApiFarm.Models;

public class NFSaida
{
    public int Id { get; set; }
    public DateTime data { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }
    public int NumeroNota { get; set; }
    public decimal Total { get; set; }
    public decimal Impostos { get; set; }
    public string Status { get; set; }
    public string Observacoes { get; set; }
}