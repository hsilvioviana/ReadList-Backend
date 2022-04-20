using Microsoft.AspNetCore.Mvc;
using ReadList.Application.ViewModels;
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

    [HttpPost("Criar")]
    public async Task<object> Post([FromQuery] CriarTesteFluxoViewModel model)
    {
        return await _service.Criar(model);
    }
}
