using System;

namespace GameStore.DTOs
{
    public class UpdateDriverDto
    {
        public int? DriverNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Acronym { get; set; }
        public string? TeamName { get; set; }
    }
}