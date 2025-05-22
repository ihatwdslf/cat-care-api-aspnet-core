using CatCareApi.Data;
using CatCareApi.DTOs.Pet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatCareApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly CatCareContext _context;

    public PetsController(CatCareContext context)
    {
        _context = context;
    }

    // GET: api/pets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetGetDto>>> GetAll()
    {
        var pets = await _context.Pets
            .Include(p => p.Owner)
            .Select(p => new PetGetDto(
                p.Id,
                p.Name,
                p.Type,
                p.Breed,
                p.Temperament,
                p.HasProsthesis,
                p.IsLazy,
                p.LikesToEat,
                p.OwnerId))
            .ToListAsync();

        return Ok(pets);
    }

    // GET: api/pets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PetGetDto>> GetById(int id)
    {
        var pet = await _context.Pets
            .Include(p => p.Owner)
            .Where(p => p.Id == id)
            .Select(p => new PetGetDto(
                p.Id,
                p.Name,
                p.Type,
                p.Breed,
                p.Temperament,
                p.HasProsthesis,
                p.IsLazy,
                p.LikesToEat,
                p.OwnerId))
            .FirstOrDefaultAsync();

        if (pet == null) return NotFound();

        return Ok(pet);
    }

    // POST: api/pets
    [HttpPost]
    public async Task<ActionResult<PetGetDto>> Create(PetCreateDto dto)
    {
        // Перевірка чи існує власник (OwnerId) потрібен тут, бо він потрібен для створення Pet
        // В нашій логіці власник створюється автоматично при створенні заявки, 
        // але для прямої роботи з Pets додамо параметр OwnerId у DTO, або можна додати його в контролер (тут додамо в DTO)

        return BadRequest("Direct pet creation requires OwnerId. Use Applications endpoint to create pet with owner.");
    }

    // PUT: api/pets/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PetUpdateDto dto)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null) return NotFound();

        pet.Name = dto.Name;
        pet.Type = dto.Type;
        pet.Breed = dto.Breed;
        pet.Temperament = dto.Temperament;
        pet.HasProsthesis = dto.HasProsthesis;
        pet.IsLazy = dto.IsLazy;
        pet.LikesToEat = dto.LikesToEat;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}