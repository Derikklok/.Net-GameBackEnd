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
}
