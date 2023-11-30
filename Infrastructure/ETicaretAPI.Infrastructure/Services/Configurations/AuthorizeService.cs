using ETicaretAPI.Application.Abstraction.Services.Configurations;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Dtos.Configuration;
using ETicaretAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ETicaretAPI.Infrastructure.Services.Configurations
{
    public class AuthorizeService : IAuthorizeService
    {
        public List<AuthorizeMenu> GetAuthorizeDefinitionEndpoints(Type assemblyType)
        {
            Assembly assembly = Assembly.GetAssembly(assemblyType);
            //IsAssignableTo kalıtım ile altsınıfları getiriyor
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
            List<AuthorizeMenu> menus = new();

            if (controllers != null)

                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                AuthorizeMenu menu = null;
                                var authorizedDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if (!menus.Any(m => m.Name == authorizedDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizedDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }

                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authorizedDefinitionAttribute.Menu);

                                AuthorizeAction authorizeAciton = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizedDefinitionAttribute.ActionType),
                                    Definition = authorizedDefinitionAttribute.Definition,
                                };

                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                    authorizeAciton.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    authorizeAciton.HttpType = HttpMethods.Get;

                                authorizeAciton.Code = $"{authorizeAciton.HttpType}.{authorizeAciton.ActionType}.{authorizeAciton.Definition.Replace(" ", "")}";
                                menu.AuthorizeActions.Add(authorizeAciton);
                            }

                        }
                }

            return menus;
        }
    }
}
