using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReadList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpPost("TestJWT")]
        [Authorize]
        public ActionResult<string> TestJwt()
        {
            var userId = User.FindFirst("id")?.Value;
            return $"JWT passou, User Id = {userId}";
        }
    }
}
