using System;
using GameStore.Models;
using System.Collections.Generic;

namespace GameStore.DTOs;

public class OfficerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public List<ApplicationDto> Applications { get; set; } = new List<ApplicationDto>();

    // Static method to convert from Officer model to OfficerDto
    public static OfficerDto FromOfficer(Officer officer)
    {
        var officerDto = new OfficerDto
        {
            Id = officer.Id,
            FullName = officer.FullName,
            Department = officer.Department
        };

        if (officer.Applications != null)
        {
            foreach (var app in officer.Applications)
            {
                officerDto.Applications.Add(new ApplicationDto
                {
                    Id = app.Id,
                    ApplicationType = app.ApplicationType,
                    SubmittedTime = app.SubmittedTime,
                    Status = app.Status
                });
            }
        }

        return officerDto;
    }
}