using Blog.Identity.Application.Features.User.Commands.CreateUser;
using Blog.Identity.Application.Features.User.Commands.DeleteUser;
using Blog.Identity.Application.Features.User.Commands.UpdateUser;
using Blog.Identity.Application.Features.User.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Identity.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody]CreateUserCommand request, CancellationToken ct = default)
        {
            return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request, ct));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginQuery([FromBody]LoginQuery loginQuery, CancellationToken ct = default)
        {
            return Ok(await _mediator.Send(loginQuery, ct));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync([FromBody]UpdateUserCommand request, CancellationToken ct = default)
        {
            await _mediator.Send(request, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync([FromRoute]Guid id, CancellationToken ct = default)
        {
            await _mediator.Send(new DeleteUserCommandById(id), ct);
            return NoContent();
        }
    }
}
