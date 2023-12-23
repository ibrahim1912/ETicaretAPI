using ETicaretAPI.Application.Features.Commands.AppUser.AuthorizationEndpoint.AssignRoleEndpoint;
using ETicaretAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationEndpointsController : BaseController
{
    [HttpPost("[action]")]

    public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
    {
        GetRolesToEndpointQueryResponse response = await Mediator.Send(getRolesToEndpointQueryRequest);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
    {
        assignRoleEndpointCommandRequest.Type = typeof(Program);
        AssignRoleEndpointCommandResponse response = await Mediator.Send(assignRoleEndpointCommandRequest);

        return Ok(response);
    }
}
