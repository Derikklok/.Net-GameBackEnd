using System;
using GameStore.Models;
using System.Collections.Generic;

namespace GameStore.DTOs;

public class CitizenDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string NIC { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public List<ApplicationDto> Applications { get; set; } = new List<ApplicationDto>();

    // Static method to convert from Citizen model to CitizenDto
    public static CitizenDto FromCitizen(Citizen citizen)
    {
        var citizenDto = new CitizenDto
        {
            Id = citizen.Id,
            FullName = citizen.FullName,
            NIC = citizen.NIC,
            Address = citizen.Address,
            ContactNumber = citizen.ContactNumber
        };

        if (citizen.Applications != null)
        {
            foreach (var app in citizen.Applications)
            {
                citizenDto.Applications.Add(new ApplicationDto
                {
                    Id = app.Id,
                    ApplicationType = app.ApplicationType,
                    SubmittedTime = app.SubmittedTime,
                    Status = app.Status
                });
            }
        }

        return citizenDto;
    }
}

public class ApplicationDto
{
    public int Id { get; set; }
    public string ApplicationType { get; set; } = string.Empty;
    public DateTime SubmittedTime { get; set; }
    public string Status { get; set; } = string.Empty;
}