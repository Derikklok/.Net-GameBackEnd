using System;
using GameStore.Data;
using GameStore.Models;
using GameStore.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrandPrixController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GrandPrixController(AppDbContext context)
        {
            _context = context;
        }

        // GET : api/grandprix
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrandPrix>>> GetGrandPrixes()
        {
            return await _context.GrandPrix
                .Include(gp => gp.Participations)
                .ThenInclude(p => p.Driver)
                .ToListAsync();
        }

        // GET : api/grandprix/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GrandPrix>> GetGrandPrix(int id)
        {
            var gp = await _context.GrandPrix
                .Include(gp => gp.Participations)
                .ThenInclude(p => p.Driver)
                .FirstOrDefaultAsync(gp => gp.Id == id);
            
            if (gp == null) return NotFound();
            return gp;
        }

        // POST : api/garndprix
        [HttpPost]
        public async Task<ActionResult<GrandPrix>> CreateGrandprix(GrandPrix grandPrix)
        {
            _context.GrandPrix.Add(grandPrix);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGrandPrix), new { id = grandPrix.Id }, grandPrix);
        }

        // PUT : api/grandprix/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GrandPrix>> UpdateGrandPrix(int id, UpdateGrandPrixDto updateDto)
        {
            // Find the existing grand prix
            var existingGrandPrix = await _context.GrandPrix.FindAsync(id);
            if (existingGrandPrix == null)
                return NotFound();

            // Update only the properties that are provided (not null)
            if (updateDto.Name != null)
                existingGrandPrix.Name = updateDto.Name;
                
            if (updateDto.Location != null)
                existingGrandPrix.Location = updateDto.Location;
                
            if (updateDto.Laps.HasValue)
                existingGrandPrix.Laps = updateDto.Laps.Value;
                
            if (updateDto.Length.HasValue)
                existingGrandPrix.Length = updateDto.Length.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            // Return the updated grand prix
            return existingGrandPrix;
        }

        // DELETE : api/grandprix/5
         [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrandPrix(int id)
        {
            var gp = await _context.GrandPrix.FindAsync(id);
            if (gp == null) return NotFound();
            _context.GrandPrix.Remove(gp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


