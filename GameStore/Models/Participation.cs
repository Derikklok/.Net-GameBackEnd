using System;

namespace GameStore.Models
{
    public class Participation
    {
        public int Id { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;
        
        public int GrandPrixId { get; set;}
        public GrandPrix GrandPrix { get; set; } = null!;

    }
}


