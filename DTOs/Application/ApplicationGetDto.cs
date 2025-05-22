namespace CatCareApi.DTOs.Application;

public record ApplicationGetDto(
    int Id,
    int PetId,
    int OwnerId,
    int? ServiceTypeId,
    int? CaretakerId,
    string? AdditionalNotes,
    DateTime CreatedAt);