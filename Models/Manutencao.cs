namespace MvcApiFarm.Models;

public class Manutencao
{
    public int Id { get; set; }
    public int RecursoId { get; set; }
    public required Recurso Recurso { get; set; }
    public int FuncionarioId { get; set; }
    public required Funcionario Funcionario { get; set; }

    public DateTime Data { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    public decimal Custos { get; set; }
    public string Status { get; set; }
}