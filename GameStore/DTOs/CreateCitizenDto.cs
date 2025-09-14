using System;

namespace GameStore.DTOs;

public class CreateCitizenDto
{
    public string FullName { get; set; } = string.Empty;
    public string NIC { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
}