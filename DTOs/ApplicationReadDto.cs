namespace CatCareApi.DTOs;

public class ApplicationReadDto
{
    public int Id { get; set; }
    public string PetType { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public string? Temperament { get; set; }
    public bool HasProsthesis { get; set; }
    public bool IsLazy { get; set; }
    public bool LikesToEat { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? PreferredService { get; set; }
    public string? AdditionalNotes { get; set; }
}