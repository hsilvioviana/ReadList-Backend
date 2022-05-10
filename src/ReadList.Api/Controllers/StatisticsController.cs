using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadList.Application.ViewModels;
using ReadList.Services.Interfaces;

namespace ReadList.Api.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        protected readonly IStatisticsService _service;

        public StatisticsController (IStatisticsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<StatisticsViewModel> Get()
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.Statistics(new Guid(userId));
        }
    }
}
