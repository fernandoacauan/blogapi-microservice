using Blog.Identity.Application.Features.Role.Commands.CreateRole;
using Blog.Identity.Application.Features.Role.Commands.DeleteRoleById;
using Blog.Identity.Application.Features.Role.Commands.UpdateRoleById;
using Blog.Identity.Application.Features.Role.Queries.GetAllRoles;
using Blog.Identity.Application.Features.Role.Queries.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Identity.Presentation.Controllers
{
    [Authorize(Policy = "IsAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRolesAsync(CancellationToken ct = default)
        {
            return Ok(await _mediator.Send(new GetAllRolesQuery(), ct));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRoleByIdAsync([FromBody]UpdateRoleByIdCommand update, CancellationToken ct = default)
        {
            await _mediator.Send(update, ct);
            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken ct = default)
        {
            return Ok(await _mediator.Send(new GetRoleByIdQuery(id), ct));
        }

        [HttpPost]
        public async Task<ActionResult> CreateRoleAsync([FromBody]CreateRoleCommand create, CancellationToken ct = default)
        {
            Guid id = await _mediator.Send(create, ct);

            return StatusCode(StatusCodes.Status201Created, id);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteRoleAsync([FromRoute]Guid id, CancellationToken ct = default)
        {
            await _mediator.Send(new DeleteRoleByIdCommand(id), ct);
            return NoContent();
        }
    }
}
