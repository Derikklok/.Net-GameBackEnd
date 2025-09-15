using System;
using GameStore.Data;
using GameStore.Models;
using GameStore.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitizensController : ControllerBase
{
    private readonly AppDbContext _context;

    public CitizensController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/citizens
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CitizenDto>>> GetCitizens()
    {
        var citizens = await _context.Citizens
            .Include(c => c.Applications)
            .ToListAsync();

        return citizens.Select(CitizenDto.FromCitizen).ToList();
    }

    // GET: api/citizens/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CitizenDto>> GetCitizen(int id)
    {
        var citizen = await _context.Citizens
            .Include(c => c.Applications)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (citizen == null)
            return NotFound();

        return CitizenDto.FromCitizen(citizen);
    }

    // POST: api/citizens
    [HttpPost]
    public async Task<ActionResult<CitizenDto>> AddCitizen(CreateCitizenDto citizenDto)
    {
        var citizen = new Citizen
        {
            FullName = citizenDto.FullName,
            NIC = citizenDto.NIC,
            Address = citizenDto.Address,
            ContactNumber = citizenDto.ContactNumber,
            Applications = new List<Application>() // Initialize with empty list
        };

        _context.Citizens.Add(citizen);
        await _context.SaveChangesAsync();

        var createdCitizenDto = CitizenDto.FromCitizen(citizen);
        return CreatedAtAction(nameof(GetCitizen), new { id = citizen.Id }, createdCitizenDto);
    }

    // PUT: api/citizens/5
    [HttpPut("{id}")]
    public async Task<ActionResult<CitizenDto>> UpdateCitizen(int id, UpdateCitizenDto updateDto)
    {
        var citizen = await _context.Citizens
            .Include(c => c.Applications)
            .FirstOrDefaultAsync(c => c.Id == id);
            
        if (citizen == null)
            return NotFound();

        // Update only the properties that are not null
        if (updateDto.FullName != null)
            citizen.FullName = updateDto.FullName;
            
        if (updateDto.NIC != null)
            citizen.NIC = updateDto.NIC;
            
        if (updateDto.Address != null)
            citizen.Address = updateDto.Address;
            
        if (updateDto.ContactNumber != null)
            citizen.ContactNumber = updateDto.ContactNumber;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!CitizenExists(id))
        {
            return NotFound();
        }

        // Return the updated citizen data
        return CitizenDto.FromCitizen(citizen);
    }

    // DELETE: api/citizens/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCitizen(int id)
    {
        var citizen = await _context.Citizens.FindAsync(id);
        if (citizen == null)
            return NotFound();

        _context.Citizens.Remove(citizen);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CitizenExists(int id)
    {
        return _context.Citizens.Any(c => c.Id == id);
    }
}
