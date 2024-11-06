namespace MvcApiFarm.Models;

public class Funcionario
{
    public int Id { get; set; }
    public int FazendaId { get; set; }
    public required Fazenda Fazenda { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Funcao { get; set; }
    public decimal? Salario { get; set; }
    public string? Senha { get; set; }
}