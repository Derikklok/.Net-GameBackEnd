using System;

namespace GameStore.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public int DriverNumber { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Acronym { get; set; } = null!;
        public string? TeamName { get; set; }

        public ICollection<Participation> Participations { get; set; } = new List<Participation>();
    }
}


