using ETicaretAPI.Application.Abstraction.Services.Configurations;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AuthorizeServicesController : ControllerBase
    {
        readonly IAuthorizeService _authorizeService;

        public AuthorizeServicesController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefintionConstants.AuthorizeServices, ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var datas = _authorizeService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
