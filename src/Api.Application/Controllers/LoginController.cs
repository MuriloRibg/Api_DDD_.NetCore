using System.Net;
using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.DataTransfer;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<object> Login(
            [FromBody] LoginRequests userRequests,
            [FromServices] ILoginService service
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (userRequests is null)
            {
                return BadRequest();
            }

            try
            {
                var result = await service.FindByLogin(userRequests);
                if (result is not null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
