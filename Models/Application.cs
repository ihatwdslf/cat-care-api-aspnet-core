using System.ComponentModel.DataAnnotations;

namespace CatCareApi.Models;

public class Application
{
    public int Id { get; set; }

    [Required] public int PetId { get; set; }

    public Pet Pet { get; set; } = null!;

    [Required] public int OwnerId { get; set; }

    public Owner Owner { get; set; } = null!;

    public int? ServiceTypeId { get; set; }
    public ServiceType? ServiceType { get; set; }

    public int? CaretakerId { get; set; }
    public Caretaker? Caretaker { get; set; }

    public string? AdditionalNotes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}