using System.ComponentModel.DataAnnotations;

namespace CatCareApi.Models;

public class ServiceType
{
    public int Id { get; set; }

    [Required] public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}