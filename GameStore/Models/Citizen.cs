using System;

namespace GameStore.Models;

public class Citizen
{
    public int Id { get; set; }
    public required string FullName { get; set; }

    public required string NIC { get; set; }

    public required string Address { get; set; }
    public required string ContactNumber { get; set; }

    // One Citizen can have many applications
    public required List<Application> Applications{ get; set; }
}
