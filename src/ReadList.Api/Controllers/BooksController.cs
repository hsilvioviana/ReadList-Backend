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

        // GET api/books/search
        /// <summary>
        /// Busca de livros divididos por ano.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("search")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> Search()
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.SearchDividedByYear(new Guid(userId));
        }

        // GET api/books/search/{id}
        /// <summary>
        /// Busca de livro por id.
        /// </summary>
        /// <param name="id">Id do livro</param>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("search/{id}")]
        [Authorize]
        public async Task<BookViewModel> Find(string id)
        {
            var userId = User.FindFirst("id")?.Value;

            var viewModel = new FindBookViewModel()
            {
                UserId = new Guid(userId),
                Id = new Guid(id) 
            };

            return await _service.Find(viewModel);
        }

        // POST api/books/create
        /// <summary>
        /// Cadastro de livro.
        /// </summary>
        /// <remarks>
        /// Exemplo de body:
        /// 
        ///     {
        ///         "title": "O Pequeno Príncipe",
        ///         "author": "Antoine de Saint-Exupéry",
        ///         "releaseYear": 1943,
        ///         "readingYear": 2022,
        ///         "isFiction": true,
        ///         "genres": [
        ///             "Romance",
        ///             "Aventura", 
        ///             "Infantil"
        ///         ],
        ///         "numberOfPages": 132,
        ///         "countryOfOrigin": "França",
        ///         "language": "Português"
        ///     }
        /// </remarks>
        /// <param name="viewModel">Dados de criação do livro.</param>
        /// <response code="200">Cadastro realizado com sucesso.</response>
        /// <response code="400">Erro no cadastro.</response>
        [HttpPost("create")]
        [Authorize]
        public async Task Create([FromBody] CreateBookViewModel viewModel)
        {
            var userId = User.FindFirst("id")?.Value;

            viewModel.UserId = new Guid(userId);

            await _service.Create(viewModel);
        }

        // PUT api/books/update/{id}
        /// <summary>
        /// Atualização de livro.
        /// </summary>
        /// <remarks>
        /// Exemplo de body:
        /// 
        ///     {
        ///         "title": "O Pequeno Príncipe",
        ///         "author": "Antoine de Saint-Exupéry",
        ///         "releaseYear": 1943,
        ///         "readingYear": 2022,
        ///         "isFiction": true,
        ///         "genres": [
        ///             "Romance",
        ///             "Aventura", 
        ///             "Infantil",
        ///             "Filosofia"
        ///         ],
        ///         "numberOfPages": 132,
        ///         "countryOfOrigin": "França",
        ///         "language": "Português"
        ///     }
        /// </remarks>
        /// <param name="viewModel">Dados de atualização do livro.</param>
        /// <param name="id">Id do livro.</param>
        /// <response code="200">Atualização realizada com sucesso.</response>
        /// <response code="400">Erro na atualização.</response>
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task Update([FromBody] UpdateBookViewModel viewModel, string id)
        {
            var userId = User.FindFirst("id")?.Value;

            viewModel.UserId = new Guid(userId);
            viewModel.Id = new Guid(id);

            await _service.Update(viewModel);
        }

        // DELETE api/books/delete/{id}
        /// <summary>
        /// Remoção de livro por id.
        /// </summary>
        /// <param name="id">Id do livro.</param>
        /// <response code="200">Remoção realizada com sucesso.</response>
        /// <response code="400">Erro na remoção.</response>
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
