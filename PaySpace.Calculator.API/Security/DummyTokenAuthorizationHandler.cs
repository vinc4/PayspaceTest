using Microsoft.AspNetCore.Authorization;

namespace PaySpace.Calculator.API.Security
{
    public class DummyTokenAuthorizationHandler : AuthorizationHandler<IAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            // Check if the request contains the dummy token
            if (context.User.Identity.IsAuthenticated && context.User.HasClaim(c => c.Type == "Dummy-Token"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
