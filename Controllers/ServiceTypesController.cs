using CatCareApi.Data;
using CatCareApi.DTOs.ServiceType;
using CatCareApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatCareApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceTypesController : ControllerBase
{
    private readonly CatCareContext _context;

    public ServiceTypesController(CatCareContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceTypeGetDto>>> Get()
    {
        return await _context.ServiceTypes.Select(s => new ServiceTypeGetDto(s.Id, s.Name, s.Description))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<ServiceTypeGetDto>> Create(ServiceTypeCreateDto dto)
    {
        var model = new ServiceType { Name = dto.Name, Description = dto.Description };
        _context.ServiceTypes.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id },
            new ServiceTypeGetDto(model.Id, model.Name, model.Description));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ServiceTypeUpdateDto dto)
    {
        var entity = await _context.ServiceTypes.FindAsync(id);
        if (entity == null) return NotFound();
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}