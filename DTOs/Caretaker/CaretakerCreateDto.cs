namespace CatCareApi.DTOs.Caretaker;

public record CaretakerCreateDto(string FullName, string? PhoneNumber, string? Notes);