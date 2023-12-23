using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Features.Commands.AppUser.AssignRoleToUser;
using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;
using ETicaretAPI.Application.Features.Commands.AppUser.HasRoleUser;
using ETicaretAPI.Application.Features.Commands.AppUser.UpdatePassword;
using ETicaretAPI.Application.Features.Commands.AppUser.UpdateUser;
using ETicaretAPI.Application.Features.Queries.AppUser.GetByIdUser;
using ETicaretAPI.Application.Features.Queries.AppUser.GetRolesToUser;
using ETicaretAPI.Application.Features.Queries.AppUser.GettAllUsers;
using ETicaretAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        readonly IUserService _userService;

        public UsersController(IUserService userService)
        {

            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await Mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await Mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition
            (
            Menu = AuthorizeDefinitionConstants.AuthorizeDefinitionMenu.Users,
            ActionType = ActionType.Reading,
            Definition = AuthorizeDefinitionConstants.AuthorizeDefinitionName.GetAllUsers
            )
        ]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await Mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }


        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition
            (
            Menu = AuthorizeDefinitionConstants.AuthorizeDefinitionMenu.Users,
            ActionType = ActionType.Reading,
            Definition = AuthorizeDefinitionConstants.AuthorizeDefinitionName.AssignRoleToUser
            )
        ]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await Mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }


        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition
            (
            Menu = AuthorizeDefinitionConstants.AuthorizeDefinitionMenu.Users,
            ActionType = ActionType.Reading,
            Definition = AuthorizeDefinitionConstants.AuthorizeDefinitionName.GetRolesToUser
            )
        ]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await Mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);
        }


        [HttpPost("has-role-user")]
        public async Task<IActionResult> HasUserRole(HasRoleUserCommandRequest hasRoleUserCommandRequest)
        {
            HasRoleUserCommandResponse response = await Mediator.Send(hasRoleUserCommandRequest);

            return Ok(response);
        }

        [HttpGet("{Token}")]
        public async Task<IActionResult> GetByIdUser([FromRoute] GetByIdUserQueryRequest getByIdUserQueryRequest)
        {
            GetByIdUserQueryResponse response = await Mediator.Send(getByIdUserQueryRequest);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest updateUserCommandRequest)
        {
            UpdateUserCommandResponse response = await Mediator.Send(updateUserCommandRequest);
            return Ok(response);
        }
    }
}

