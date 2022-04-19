using Microsoft.AspNetCore.Mvc;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;

namespace ReadList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TesteFluxoController : ControllerBase
{
    ITesteFluxoService _service;
    public TesteFluxoController (ITesteFluxoService service)
    {
        _service = service;
    }

    [HttpPost("Adicionar")]
    public async Task<object> Post([FromQuery] TesteFluxoModel model)
    {
        return await _service.AdicionarTesteFluxo(model);
    }
}
