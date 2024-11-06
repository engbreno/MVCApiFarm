namespace MvcApiFarm.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Endereco { get; set; }
    public DateTime Data_Nascimento { get; set; }
    public string Telefone { get; set; }
}