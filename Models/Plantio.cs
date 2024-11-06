using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MvcApiFarm.Models;

public class Plantio
{
    public int Id { get; set; }
    public DateTime DataPlantio { get; set; }

    public int AreaPlantioId { get; set; }

    [ValidateNever] [JsonIgnore] public AreaPlantio AreaPlantio { get; set; }

    public ICollection<ItemPlantio> ItensPlantio { get; set; }
}