using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Security
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {        
        
    }

    public class IsAdminRequirementHandler : AuthorizationHandler<IsAdminRequirement>
    {
        public IsAdminRequirementHandler()
        {
            
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.Name);
            Console.WriteLine("userId: " + userId);
            if(userId == null) return Task.CompletedTask;

            if(userId == "admin")  //If admin then always allow, I know its ugly! But no time :D
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            
            return Task.CompletedTask;
        }

    }
}