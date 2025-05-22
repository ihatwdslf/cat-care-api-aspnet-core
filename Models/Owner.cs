using System.ComponentModel.DataAnnotations;

namespace CatCareApi.Models;

public class Owner
{
    public int Id { get; set; }

    [Required] public string FullName { get; set; } = string.Empty;

    [Required] [Phone] public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}