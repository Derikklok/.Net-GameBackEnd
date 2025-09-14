using System;

namespace GameStore.Models;

public class Application
{
    public int Id { get; set; }

    public required string ApplicationType { get; set; }

    public DateTime SubmittedTime { get; set; }

    public string Status { get; set; } = "Pending";

    // Relationships
    public int CitizenId { get; set; }
    public required Citizen Citizen { get; set; }
    
     public int OfficerId { get; set; }
    public required Officer Officer { get; set; }
}
