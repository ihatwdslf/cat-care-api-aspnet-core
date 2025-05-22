using CatCareApi.Data;
using CatCareApi.DTOs.Caretaker;
using CatCareApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatCareApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaretakersController : ControllerBase
{
    private readonly CatCareContext _context;

    public CaretakersController(CatCareContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CaretakerGetDto>>> Get()
    {
        return await _context.Caretakers.Select(c => new CaretakerGetDto(c.Id, c.FullName, c.PhoneNumber, c.Notes))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<CaretakerGetDto>> Create(CaretakerCreateDto dto)
    {
        var model = new Caretaker { FullName = dto.FullName, PhoneNumber = dto.PhoneNumber, Notes = dto.Notes };
        _context.Caretakers.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id },
            new CaretakerGetDto(model.Id, model.FullName, model.PhoneNumber, model.Notes));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CaretakerUpdateDto dto)
    {
        var entity = await _context.Caretakers.FindAsync(id);
        if (entity == null) return NotFound();
        entity.FullName = dto.FullName;
        entity.PhoneNumber = dto.PhoneNumber;
        entity.Notes = dto.Notes;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}