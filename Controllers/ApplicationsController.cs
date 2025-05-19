using CatCareApi.Data;
using CatCareApi.DTOs;
using CatCareApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatCareApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ApplicationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/applications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationReadDto>>> GetAll()
    {
        var applications = await _context.Applications.ToListAsync();

        var dtos = applications.Select(a => new ApplicationReadDto
        {
            Id = a.Id,
            PetType = a.PetType,
            Breed = a.Breed,
            Temperament = a.Temperament,
            HasProsthesis = a.HasProsthesis,
            IsLazy = a.IsLazy,
            LikesToEat = a.LikesToEat,
            PhoneNumber = a.PhoneNumber,
            PreferredService = a.PreferredService,
            AdditionalNotes = a.AdditionalNotes
        });

        return Ok(dtos);
    }
    
    // GET: api/applications/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationReadDto>> GetById(int id)
    {
        var application = await _context.Applications.FindAsync(id);
        if (application == null)
            return NotFound();

        var dto = new ApplicationReadDto
        {
            Id = application.Id,
            PetType = application.PetType,
            Breed = application.Breed,
            Temperament = application.Temperament,
            HasProsthesis = application.HasProsthesis,
            IsLazy = application.IsLazy,
            LikesToEat = application.LikesToEat,
            PhoneNumber = application.PhoneNumber,
            PreferredService = application.PreferredService,
            AdditionalNotes = application.AdditionalNotes
        };

        return Ok(dto);
    }
    
    // POST: api/applications
    [HttpPost]
    public async Task<ActionResult<ApplicationReadDto>> Create([FromBody] ApplicationCreateDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Перевірка унікальності PhoneNumber (якщо потрібно)
        var exists = await _context.Applications.AnyAsync(a => a.PhoneNumber == createDto.PhoneNumber);
        if (exists)
            return Conflict($"Application with PhoneNumber {createDto.PhoneNumber} already exists.");

        var application = new Application
        {
            PetType = createDto.PetType,
            Breed = createDto.Breed,
            Temperament = createDto.Temperament,
            HasProsthesis = createDto.HasProsthesis,
            IsLazy = createDto.IsLazy,
            LikesToEat = createDto.LikesToEat,
            PhoneNumber = createDto.PhoneNumber,
            PreferredService = createDto.PreferredService,
            AdditionalNotes = createDto.AdditionalNotes
        };

        _context.Applications.Add(application);
        await _context.SaveChangesAsync();

        var readDto = new ApplicationReadDto
        {
            Id = application.Id,
            PetType = application.PetType,
            Breed = application.Breed,
            Temperament = application.Temperament,
            HasProsthesis = application.HasProsthesis,
            IsLazy = application.IsLazy,
            LikesToEat = application.LikesToEat,
            PhoneNumber = application.PhoneNumber,
            PreferredService = application.PreferredService,
            AdditionalNotes = application.AdditionalNotes
        };

        return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
    }

    // PUT: api/applications/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ApplicationUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var application = await _context.Applications.FindAsync(id);
        if (application == null)
            return NotFound();

        application.PetType = updateDto.PetType;
        application.Breed = updateDto.Breed;
        application.Temperament = updateDto.Temperament;
        application.HasProsthesis = updateDto.HasProsthesis;
        application.IsLazy = updateDto.IsLazy;
        application.LikesToEat = updateDto.LikesToEat;
        application.PhoneNumber = updateDto.PhoneNumber;
        application.PreferredService = updateDto.PreferredService;
        application.AdditionalNotes = updateDto.AdditionalNotes;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // DELETE: api/applications/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var application = await _context.Applications.FindAsync(id);
        if (application == null)
            return NotFound();

        _context.Applications.Remove(application);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}