namespace CatCareApi.DTOs.Pet;

public record PetGetDto(
    int Id,
    string Name,
    string Type,
    string Breed,
    string? Temperament,
    bool HasProsthesis,
    bool IsLazy,
    bool LikesToEat,
    int OwnerId);