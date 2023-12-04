using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Abstraction.Services.Configurations;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IAuthorizeService _authorizeService;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IEndpointWriteRepository _endpointWriteRepository;
        readonly IAuthorizeMenuWriteRepository _authorizeMenuWriteRepository;
        readonly IAuthorizeMenuReadRepository _authorizeMenuReadRepository;
        readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndpointService(IAuthorizeService authorizeService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IAuthorizeMenuWriteRepository authorizeMenuWriteRepository, IAuthorizeMenuReadRepository authorizeMenuReadRepository, RoleManager<AppRole> roleManager)
        {
            _authorizeService = authorizeService;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _authorizeMenuWriteRepository = authorizeMenuWriteRepository;
            _authorizeMenuReadRepository = authorizeMenuReadRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string authorizeMenu, string code, Type type)
        {
            AuthorizeMenu _authorizeMenu = await _authorizeMenuReadRepository.GetSingleAsync(m => m.Name == authorizeMenu);

            if (_authorizeMenu == null)
            {
                _authorizeMenu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = authorizeMenu
                };

                await _authorizeMenuWriteRepository.AddAsync(_authorizeMenu);

                await _authorizeMenuWriteRepository.SaveAsync();
            }

            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.AuthorizeMenu)
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code && e.AuthorizeMenu.Name == authorizeMenu);

            if (endpoint == null)
            {
                var authorizeAction = _authorizeService.GetAuthorizeDefinitionEndpoints(type)
                     .FirstOrDefault(m => m.Name == authorizeMenu)
                     ?.AuthorizeActions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Code = authorizeAction.Code,
                    ActionType = authorizeAction.ActionType,
                    HttpType = authorizeAction.HttpType,
                    Definition = authorizeAction.Definition,
                    Id = Guid.NewGuid(),
                    AuthorizeMenu = _authorizeMenu
                };
                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }

            foreach (var role in endpoint.Roles)
                endpoint.Roles.Remove(role);

            var appRoles = await _roleManager.Roles
                .Where(r => roles.Contains(r.Name))
                .ToListAsync();

            foreach (var role in appRoles)
                endpoint.Roles.Add(role);

            await _endpointWriteRepository.SaveAsync();


        }

        public async Task<List<string>?> GetRolesToEndpointAsync(string code, string authorizeMenu)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .Include(e => e.AuthorizeMenu)
                .FirstOrDefaultAsync(e => e.Code == code && e.AuthorizeMenu.Name == authorizeMenu);


            return endpoint?.Roles.Select(r => r.Name).ToList();


        }
    }
}
