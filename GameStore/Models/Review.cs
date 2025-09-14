using System;

namespace GameStore.Models;

public class Review
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public required string UserName { get; set; }
    public required string Comment { get; set; }
    public int Rating { get; set; } // 1-5 star rating
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}