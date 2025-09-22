using System;

namespace GameStore.DTOs
{
    public class UpdateGrandPrixDto
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int? Laps { get; set; }
        public double? Length { get; set; }
    }
}