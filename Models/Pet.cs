using System.ComponentModel.DataAnnotations;

namespace CatCareApi.Models;

public class Pet
{
    public int Id { get; set; }

    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Type { get; set; } = "Cat";

    [Required] public string Breed { get; set; } = string.Empty;

    public string? Temperament { get; set; }

    public bool HasProsthesis { get; set; }
    public bool IsLazy { get; set; }
    public bool LikesToEat { get; set; }

    public int OwnerId { get; set; }

    public Owner Owner { get; set; } = null!;
}