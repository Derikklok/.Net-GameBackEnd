using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Models;
using GameStore.DTOs;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParticipationController(AppDbContext context)
        {
            _context = context;
        }

        // Assign driver to Grand Prix
        [HttpPost]
        public async Task<ActionResult<Participation>> AssignDriver(CreateParticipationDto participationDto)
        {
            // Prevent duplicates
            bool exists = await _context.Participation
                .AnyAsync(p => p.DriverId == participationDto.DriverId && p.GrandPrixId == participationDto.GrandPrixId);

            if (exists) return Conflict("Driver already assigned to this Grand Prix.");

            // Create the participation entity
            var participation = new Participation
            {
                DriverId = participationDto.DriverId,
                GrandPrixId = participationDto.GrandPrixId
            };

            _context.Participation.Add(participation);
            await _context.SaveChangesAsync();
            
            // Return the created participation with related data
            var createdParticipation = await _context.Participation
                .Include(p => p.Driver)
                .Include(p => p.GrandPrix)
                .FirstOrDefaultAsync(p => p.Id == participation.Id);
            
            return CreatedAtAction(nameof(GetParticipation), new { id = participation.Id }, createdParticipation);
        }

        // Get participation by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Participation>> GetParticipation(int id)
        {
            var p = await _context.Participation
                .Include(x => x.Driver)
                .Include(x => x.GrandPrix)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (p == null) return NotFound();
            return p;
        }

        // Get all drivers in a Grand Prix
        [HttpGet("grandprix/{gpId}")]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDriversByGrandPrix(int gpId)
        {
            return await _context.Participation
                .Where(p => p.GrandPrixId == gpId)
                .Select(p => p.Driver)
                .ToListAsync();
        }

        // Get all Grand Prix for a driver
        [HttpGet("driver/{driverId}")]
        public async Task<ActionResult<IEnumerable<GrandPrix>>> GetGrandPrixByDriver(int driverId)
        {
            return await _context.Participation
                .Where(p => p.DriverId == driverId)
                .Select(p => p.GrandPrix)
                .ToListAsync();
        }
    }
}


