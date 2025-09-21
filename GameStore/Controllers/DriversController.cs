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
    public class DriversController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DriversController(AppDbContext context)
        {
            _context = context;
        }

        // GET : api/drivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return await _context.Drivers.ToListAsync();
        }

        // GET : api/drivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return NotFound();
            return driver;
        }

        // POST: api/drivers
        [HttpPost]
        public async Task<ActionResult<Driver>> CreateDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
        }


        // PUT: api/drivers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Driver>> UpdateDriver(int id, UpdateDriverDto updateDto)
        {
            // Find the existing driver
            var existingDriver = await _context.Drivers.FindAsync(id);
            if (existingDriver == null)
                return NotFound();

            // Update only the properties that are provided (not null)
            if (updateDto.DriverNumber.HasValue)
                existingDriver.DriverNumber = updateDto.DriverNumber.Value;
                
            if (updateDto.FirstName != null)
                existingDriver.FirstName = updateDto.FirstName;
                
            if (updateDto.LastName != null)
                existingDriver.LastName = updateDto.LastName;
                
            if (updateDto.Acronym != null)
                existingDriver.Acronym = updateDto.Acronym;
                
            if (updateDto.TeamName != null)
                existingDriver.TeamName = updateDto.TeamName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            // Return the updated driver
            return existingDriver;
        }

        // DELETE: api/drivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return NotFound();

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}

