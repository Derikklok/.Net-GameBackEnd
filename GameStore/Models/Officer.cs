using System;

namespace GameStore.Models;

public class Officer
 {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Department { get; set; }

        // Navigation property
        public required List<Application> Applications { get; set; }
    }
