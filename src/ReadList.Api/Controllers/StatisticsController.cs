using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadList.Application.QueryParams;
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

        // GET api/statistics/resume
        /// <summary>
        /// Resumo das estatísticas.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("resume")]
        [Authorize]
        public async Task<StatisticsResumeViewModel> Resume([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.Resume(new Guid(userId), range);
        }

        // GET api/statistics/years-with-more-book
        /// <summary>
        /// Anos com mais livros.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("years-with-more-books")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> YearsWithMoreBooks([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.YearsWithMoreBooks(new Guid(userId), range);
        }

        // GET api/statistics/most-read-authors
        /// <summary>
        /// Autores mais lidos.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("most-read-authors")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> MostReadAuthors([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.MostReadAuthors(new Guid(userId), range);
        }

        // GET api/statistics/most-read-types
        /// <summary>
        /// Tipos mais lidos.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("most-read-types")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> MostReadTypes([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.MostReadTypes(new Guid(userId), range);
        }

        // GET api/statistics/most-read-genres
        /// <summary>
        /// Gêneros mais lidos.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("most-read-genres")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> MostReadGenres([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.MostReadGenres(new Guid(userId), range);
        }

        // GET api/statistics/most-read-countries
        /// <summary>
        /// Países mais lidos.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("most-read-countries")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> MostReadCountries([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.MostReadCountries(new Guid(userId), range);
        }

        // GET api/statistics/most-read-languages
        /// <summary>
        /// Idiomas mais lidos.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("most-read-languages")]
        [Authorize]
        public async Task<List<FormattedBookListViewModel>> MostReadLanguages([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.MostReadLanguages(new Guid(userId), range);
        }

        // GET api/statistics/oldest-books
        /// <summary>
        /// Livros mais antigos.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("oldest-books")]
        [Authorize]
        public async Task<List<BookViewModel>> OldestBooks([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.OldestBooks(new Guid(userId), range);
        }

        // GET api/statistics/biggest-books
        /// <summary>
        /// Maiores livros.
        /// </summary>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="400">Erro na busca.</response>
        [HttpGet("biggest-books")]
        [Authorize]
        public async Task<List<BookViewModel>> BiggestBooks([FromQuery] DateRangeQueryParam range)
        {
            var userId = User.FindFirst("id")?.Value;
            return await _service.BiggestBooks(new Guid(userId), range);
        }
    }
}
