using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[Route("api/test")]
[ApiController]
public class TestController : ControllerBase
{
    // GET: api/test
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test controller working!");
    }
    
    // POST: api/test
    [HttpPost]
    public IActionResult Post([FromBody] TestModel model)
    {
        if (model == null)
        {
            return BadRequest("Model is null");
        }
        
        return Ok($"Received: {model.Message}");
    }
}

public class TestModel
{
    public string Message { get; set; } = string.Empty;
}