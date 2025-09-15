using System;

namespace GameStore.DTOs;

public class CreateOfficerDto
{
    public string FullName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
}