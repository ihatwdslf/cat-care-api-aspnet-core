using System.ComponentModel.DataAnnotations;

namespace CatCareApi.DTOs;

public class ApplicationCreateDto
{
    [Required]
    public string PetType { get; set; } = string.Empty;

    [Required]
    public string Breed { get; set; } = string.Empty;

    public string? Temperament { get; set; }

    public bool HasProsthesis { get; set; }
    public bool IsLazy { get; set; }
    public bool LikesToEat { get; set; }

    [Required]
    [Phone(ErrorMessage = "PhoneNumber is not valid")]
    public string PhoneNumber { get; set; } = string.Empty;

    public string? PreferredService { get; set; }

    public string? AdditionalNotes { get; set; }
}