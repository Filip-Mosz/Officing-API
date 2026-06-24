using Microsoft.AspNetCore.Mvc;
using Officing_API.Services;

namespace Officing_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthorsController : ControllerBase
{

    

    /// <summary>
    /// Returns list of authors.
    /// </summary>
    /// <returns>Authors.</returns>
    [HttpGet]
    public ActionResult GetAuthors()
    {
        var result = new []
        {
            new { Name = "Filip M", Index = 84893},
            new { Name = "Filip S", Index = 83862},
            new { Name = "Grzegorz P", Index = 83860},
            new { Name = "Radosław W", Index = 87084}
        };
        return Ok(result);
    }
    
}