using Microsoft.AspNetCore.Mvc;

namespace ReadList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TesteController : ControllerBase
{

    [HttpGet("Teste")]
    public List<String> Get()
    {
        return new List<string> { "Teste", "No", "Controller" };
    }
}
