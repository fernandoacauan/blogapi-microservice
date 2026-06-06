using Blog.Identity.Application.Features.Role.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Identity.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRoleAsync([FromBody]CreateRoleCommand create, CancellationToken ct = default)
        {
            Guid id = await  _mediator.Send(create, ct);

            return StatusCode(StatusCodes.Status201Created, id);
        }
    }
}
