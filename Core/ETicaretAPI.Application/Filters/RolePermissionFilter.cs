using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ETicaretAPI.Application.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        //Gelen isteklerin hepsini karşılayıp ona göre operasyon yapıcaz
        readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = context.HttpContext.User.Identity?.Name;

            if (!string.IsNullOrEmpty(name) && name != Admin.UserName) //default admin
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor; //action ile ilgili bilgiler //controller action ismini almak için as ediyoruz
                //tanımlamış oldugumuz attribute bilgilerini elde etmemiz lazım
                var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                var httpAttribute = descriptor.MethodInfo.GetCustomAttributes(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

                var httpAttribute2 = descriptor.MethodInfo.GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(HttpMethodAttribute) || a.GetType().BaseType == typeof(HttpMethodAttribute)) as HttpMethodAttribute;


                var code = $"{(httpAttribute2 != null ? httpAttribute2.HttpMethods.First() : HttpMethods.Get)}.{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";


                var hasRole = await _userService.HasRolePermissionToEndpointAsync(name, code);
                if (!hasRole)

                    context.Result = new UnauthorizedResult();
                else
                    await next();
            }

            else
                await next();
        }
        // artık pipelene a ekleyebiliriz 
        //AddControllera ekliyoruz
    }
}
