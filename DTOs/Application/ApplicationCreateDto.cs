namespace CatCareApi.DTOs.Application;

public record ApplicationCreateDto(
    string OwnerFullName,
    string OwnerPhoneNumber,
    string PetName,
    string PetType,
    string PetBreed,
    string? PetTemperament,
    bool HasProsthesis,
    bool IsLazy,
    bool LikesToEat,
    int? ServiceTypeId,
    int? CaretakerId,
    string? AdditionalNotes
);