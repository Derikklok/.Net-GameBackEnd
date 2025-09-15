using System;
using GameStore.Models;

namespace GameStore.DTOs;

public class CreateApplicationDto
{
    public required string applicationType { get; set; }
    public DateTime submittedDate { get; set; }
    public string status { get; set; } = "Pending";
    public int citizenId { get; set; }
    public int officerId { get; set; }
}

public class GetApplicationDto
{
    public int Id { get; set; }
    public string ApplicationType { get; set; } = string.Empty;
    public DateTime SubmittedTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public int CitizenId { get; set; }
    public int OfficerId { get; set; }
    public CitizenBasicInfoDto? Citizen { get; set; }
    public OfficerBasicInfoDto? Officer { get; set; }

    // Static method to convert from Application model to GetApplicationDto
    public static GetApplicationDto FromApplication(Application application)
    {
        var dto = new GetApplicationDto
        {
            Id = application.Id,
            ApplicationType = application.ApplicationType,
            SubmittedTime = application.SubmittedTime,
            Status = application.Status,
            CitizenId = application.CitizenId,
            OfficerId = application.OfficerId
        };

        if (application.Citizen != null)
        {
            dto.Citizen = new CitizenBasicInfoDto
            {
                Id = application.Citizen.Id,
                FullName = application.Citizen.FullName,
                NIC = application.Citizen.NIC
            };
        }

        if (application.Officer != null)
        {
            dto.Officer = new OfficerBasicInfoDto
            {
                Id = application.Officer.Id,
                FullName = application.Officer.FullName,
                Department = application.Officer.Department
            };
        }

        return dto;
    }
}

public class CitizenBasicInfoDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string NIC { get; set; } = string.Empty;
}

public class OfficerBasicInfoDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
}