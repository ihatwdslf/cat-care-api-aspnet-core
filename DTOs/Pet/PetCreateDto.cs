namespace CatCareApi.DTOs.Pet;

public record PetCreateDto(
    string Name,
    string Type,
    string Breed,
    string? Temperament,
    bool HasProsthesis,
    bool IsLazy,
    bool LikesToEat);