using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoneyManager.IntegrationTests
{
    public class MockUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, UserSettings.UserId),
                new Claim(ClaimTypes.Name, UserSettings.Name),
                new Claim(ClaimTypes.Email, UserSettings.UserEmail), 
            }));

            await next();
        }
    }
}