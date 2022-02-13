using BikeRental.Common;
using BikeRental.DTO.DTO.Account;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BikeRental.Filters.ActionFilters
{
    public class UserRoleAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            var context = filterContext.HttpContext;
            ClaimsDTO CLAIMS = new();
            if (context.User != null)
            {
                CLAIMS.UserRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            }
            if (!CLAIMS.UserRole.IsNullEmpty() && CLAIMS.UserRole == "2")
            {
                await next();
            }
            else
            {
                filterContext.Result = new ForbiddenObjectResult(filterContext.ModelState);
                return;
            }
        }
    }
}
