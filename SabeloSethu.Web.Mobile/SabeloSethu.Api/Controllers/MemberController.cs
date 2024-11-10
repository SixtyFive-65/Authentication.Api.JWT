using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SabeloSethu.Api.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]

    public class MemberController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
