using System;
using GameStore.Data;
using GameStore.Models;
using GameStore.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfficersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OfficersController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/officers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OfficerDto>>> GetOfficers()
    {
        var officers = await _context.Officers
            .Include(o => o.Applications)
            .ToListAsync();
            
        return officers.Select(OfficerDto.FromOfficer).ToList();
    }

    // GET: api/officers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OfficerDto>> GetOfficer(int id)
    {
        var officer = await _context.Officers
            .Include(o => o.Applications)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (officer == null)
            return NotFound();

        return OfficerDto.FromOfficer(officer);
    }

    // POST api/officers for creating officers
    [HttpPost]
    public async Task<ActionResult<OfficerDto>> AddOfficer(CreateOfficerDto officerDto)
    {
        var officer = new Officer
        {
            FullName = officerDto.FullName,
            Department = officerDto.Department,
            Applications = new List<Application>() // Initialize with empty list
        };

        _context.Officers.Add(officer);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(GetOfficer), 
            new { id = officer.Id }, 
            OfficerDto.FromOfficer(officer)
        );
    }
    
    // PUT: api/officers/5
    [HttpPut("{id}")]
    public async Task<ActionResult<OfficerDto>> UpdateOfficer(int id, UpdateOfficerDto updateDto)
    {
        var officer = await _context.Officers
            .Include(o => o.Applications)
            .FirstOrDefaultAsync(o => o.Id == id);
            
        if (officer == null)
            return NotFound();

        // Update only the properties that are not null
        if (updateDto.FullName != null)
            officer.FullName = updateDto.FullName;
            
        if (updateDto.Department != null)
            officer.Department = updateDto.Department;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!OfficerExists(id))
        {
            return NotFound();
        }

        // Return the updated officer data
        return OfficerDto.FromOfficer(officer);
    }
    
    // DELETE: api/officers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOfficer(int id)
    {
        var officer = await _context.Officers.FindAsync(id);
        if (officer == null)
            return NotFound();

        _context.Officers.Remove(officer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OfficerExists(int id)
    {
        return _context.Officers.Any(o => o.Id == id);
    }
}
