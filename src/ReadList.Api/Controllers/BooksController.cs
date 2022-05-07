using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadList.Application.ViewModels;
using ReadList.Services.Interfaces;

namespace ReadList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        protected readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> Search()
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.SearchDividedByYear(new Guid(userId));
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task Create([FromBody] CreateBookViewModel viewModel)
        {
            var userId = User.FindFirst("id")?.Value;

            viewModel.UserId = new Guid(userId);

            await _service.Create(viewModel);
        }
    }
}
