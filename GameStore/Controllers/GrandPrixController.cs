using System;
using GameStore.Data;
using GameStore.Models;
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
            return await _context.GrandPrix.ToListAsync();
        }

        // GET : api/grandprix/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GrandPrix>> GetGrandPrix(int id)
        {
            var gp = await _context.GrandPrix.FindAsync(id);
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


