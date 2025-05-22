using CatCareApi.Data;
using CatCareApi.DTOs.Owner;
using CatCareApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatCareApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    private readonly CatCareContext _context;

    public OwnersController(CatCareContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerGetDto>>> GetOwners()
    {
        return await _context.Owners.Select(o => new OwnerGetDto(o.Id, o.FullName, o.PhoneNumber)).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<OwnerGetDto>> CreateOwner(OwnerCreateDto dto)
    {
        var owner = new Owner { FullName = dto.FullName, PhoneNumber = dto.PhoneNumber };
        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOwners), new { id = owner.Id },
            new OwnerGetDto(owner.Id, owner.FullName, owner.PhoneNumber));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOwner(int id, OwnerUpdateDto dto)
    {
        var owner = await _context.Owners.FindAsync(id);
        if (owner == null) return NotFound();

        owner.FullName = dto.FullName;
        owner.PhoneNumber = dto.PhoneNumber;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}