using CoronaVirusApi.Attributes;
using CoronaVirusApi.Models;
using CoronaVirusApi.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaVirusApi.Filters
{
    public class AuthorizationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                         ActionExecutionDelegate next)
        {
            if (context.ActionDescriptor
                 .FilterDescriptors
                 .Any(x => x.Filter is SkipAuthAttribute))
            {
                await next();
                return;
            }

            var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            var authHeader = context.HttpContext.Request.Headers["Authorization"].SingleOrDefault();

            if (authHeader != null && userService.ValidateToken(authHeader))
            {
                var needRole = context.ActionDescriptor
                   .FilterDescriptors
                   .SingleOrDefault(x => x.Filter is NeedRoleAttribute);

                if (needRole == null || ((NeedRoleAttribute)needRole.Filter).Role == userService.CurrentUser.Role)
                {
                    await next();
                    return;
                }
            }

            context.HttpContext.Response.StatusCode = 401;
        }
    }
}
