namespace CatCareApi.DTOs.Caretaker;

public record CaretakerGetDto(int Id, string FullName, string? PhoneNumber, string? Notes);