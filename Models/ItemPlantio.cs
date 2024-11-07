using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MvcApiFarm.Models;

public class ItemPlantio
{
    public int Id { get; set; }

    public int PlantioId { get; set; }

    [JsonIgnore] [ValidateNever] public Plantio Plantio { get; set; }

    public int RecursoId { get; set; }
    [JsonIgnore] [ValidateNever] public Recurso Recurso { get; set; }
    
    public int Quantidade { get; set; }
}