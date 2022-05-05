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

        [HttpPost("Search")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> Search()
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.SearchDividedByYear(new Guid(userId));
        }
    }
}
