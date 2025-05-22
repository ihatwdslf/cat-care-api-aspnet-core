using System.ComponentModel.DataAnnotations;

namespace CatCareApi.Models;

public class Caretaker
{
    public int Id { get; set; }

    [Required] public string FullName { get; set; } = string.Empty;

    [Phone] public string? PhoneNumber { get; set; }

    public string? Notes { get; set; }

    public ICollection<Application> AssignedApplications { get; set; } = new List<Application>();
}