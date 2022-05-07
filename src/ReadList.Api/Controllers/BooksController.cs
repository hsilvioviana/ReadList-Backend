using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadList.Application.ViewModels;
using ReadList.Services.Interfaces;

namespace ReadList.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        protected readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> Search()
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.SearchDividedByYear(new Guid(userId));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task Create([FromBody] CreateBookViewModel viewModel)
        {
            var userId = User.FindFirst("id")?.Value;

            viewModel.UserId = new Guid(userId);

            await _service.Create(viewModel);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task Update([FromBody] UpdateBookViewModel viewModel, string id)
        {
            var userId = User.FindFirst("id")?.Value;

            viewModel.UserId = new Guid(userId);
            viewModel.Id = new Guid(id);

            await _service.Update(viewModel);
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task Delete(string id)
        {
            var userId = User.FindFirst("id")?.Value;

            var viewModel = new DeleteBookViewModel()
            {
                UserId = new Guid(userId),
                Id = new Guid(id) 
            };

            await _service.Delete(viewModel);
        }
    }
}
