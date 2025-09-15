using System;
using GameStore.Data;
using GameStore.DTOs;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ApplicationsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/applications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetApplicationDto>>> GetApplications()
    {
        var applications = await _context.Applications
            .Include(a => a.Citizen)
            .Include(a => a.Officer)
            .ToListAsync();

        return applications.Select(GetApplicationDto.FromApplication).ToList();
    }

    // GET by id: api/applications/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetApplicationDto>> GetApplication(int id)
    {
        var application = await _context.Applications
               .Include(a => a.Citizen)
               .Include(a => a.Officer)
               .FirstOrDefaultAsync(a => a.Id == id);

        if (application == null)
            return NotFound();

        return GetApplicationDto.FromApplication(application);
    }

    // POST: api/applications
        [HttpPost]
        public async Task<ActionResult<GetApplicationDto>> AddApplication(CreateApplicationDto applicationDto)
        {
            // Find the citizen and officer by their IDs
            var citizen = await _context.Citizens.FindAsync(applicationDto.citizenId);
            var officer = await _context.Officers.FindAsync(applicationDto.officerId);
            
            if (citizen == null)
                return BadRequest($"Citizen with ID {applicationDto.citizenId} not found");
                
            if (officer == null)
                return BadRequest($"Officer with ID {applicationDto.officerId} not found");

            // Create a new application with the retrieved entities
            var application = new Application
            {
                ApplicationType = applicationDto.applicationType,
                SubmittedTime = applicationDto.submittedDate,
                Status = applicationDto.status,
                CitizenId = applicationDto.citizenId,
                Citizen = citizen,
                OfficerId = applicationDto.officerId,
                Officer = officer
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            
            var appDto = GetApplicationDto.FromApplication(application);
            return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, appDto);
        }
}
