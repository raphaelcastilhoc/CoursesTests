using CoursesTests.Checkout.Api.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CoursesTests.Checkout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] CheckoutDto dto)
        {
            return Ok();
        }
    }
}
