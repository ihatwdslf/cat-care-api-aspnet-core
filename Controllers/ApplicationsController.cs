using CatCareApi.Data;
using CatCareApi.DTOs.Application;
using CatCareApi.DTOs.Pet;
using CatCareApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CatCareApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly CatCareContext _context;

    public ApplicationsController(CatCareContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationGetDto>>> Get()
    {
        return await _context.Applications.Select(a =>
            new ApplicationGetDto(a.Id, a.PetId, a.OwnerId, a.ServiceTypeId, a.CaretakerId, a.AdditionalNotes,
                a.CreatedAt)).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationGetDto>> Create(ApplicationCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Step 1: Handle Owner
            var existingOwner = await _context.Owners
                .FirstOrDefaultAsync(o => o.PhoneNumber == dto.OwnerPhoneNumber);

            if (existingOwner == null)
            {
                existingOwner = new Owner
                {
                    FullName = dto.OwnerFullName,
                    PhoneNumber = dto.OwnerPhoneNumber
                };
                _context.Owners.Add(existingOwner);
                await _context.SaveChangesAsync();

                // Reload the entity to ensure we have the generated ID
                await _context.Entry(existingOwner).ReloadAsync();
            }

            // Step 2: Create Pet
            var pet = new Pet
            {
                Name = dto.PetName,
                Type = dto.PetType,
                Breed = dto.PetBreed,
                Temperament = dto.PetTemperament,
                HasProsthesis = dto.HasProsthesis,
                IsLazy = dto.IsLazy,
                LikesToEat = dto.LikesToEat,
                OwnerId = existingOwner.Id
            };

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            // Reload the pet entity to ensure we have the generated ID
            await _context.Entry(pet).ReloadAsync();

            // Step 3: Create Application
            var app = new Application
            {
                PetId = pet.Id,
                OwnerId = existingOwner.Id,
                ServiceTypeId = dto.ServiceTypeId,
                CaretakerId = dto.CaretakerId,
                AdditionalNotes = dto.AdditionalNotes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Applications.Add(app);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return CreatedAtAction(nameof(Get), new { id = app.Id },
                new ApplicationGetDto(app.Id, app.PetId, app.OwnerId, app.ServiceTypeId, app.CaretakerId,
                    app.AdditionalNotes, app.CreatedAt));
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            // Add more detailed logging
            Console.WriteLine("Error creating application. DTO: " + dto);

            // Check if it's a foreign key constraint error
            if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
                return BadRequest("Invalid reference data. Please check that the ServiceType and Caretaker exist.");

            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ApplicationUpdateDto dto)
    {
        var entity = await _context.Applications.FindAsync(id);
        if (entity == null) return NotFound();

        entity.ServiceTypeId = dto.ServiceTypeId;
        entity.CaretakerId = dto.CaretakerId;
        entity.AdditionalNotes = dto.AdditionalNotes;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}