namespace CatCareApi.DTOs.Application;

public record ApplicationUpdateDto(int? ServiceTypeId, int? CaretakerId, string? AdditionalNotes);