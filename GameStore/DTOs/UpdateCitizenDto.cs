using System;

namespace GameStore.DTOs;

public class UpdateCitizenDto
{
    public string? FullName { get; set; }
    public string? NIC { get; set; }
    public string? Address { get; set; }
    public string? ContactNumber { get; set; }
}