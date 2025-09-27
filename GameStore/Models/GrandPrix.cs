using System;
using System.Text.Json.Serialization;

namespace GameStore.Models
{
    public class GrandPrix
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public int? Laps { get; set; }
        public double? Length { get; set; }

        [JsonIgnore]
        public ICollection<Participation> Participations { get; set; } = new List<Participation>();
    }
}


