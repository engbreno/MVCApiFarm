namespace MvcApiFarm.Models;

public class Recurso
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public string UnidadeMedida { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }

    // Propriedades específicas para Maquinário
    public string? NumeroSerie { get; set; } // Número de série para maquinários
    public DateTime DataAquisicao { get; set; } // Data de aquisição
    public string? Status { get; set; } //status do maquinario
}