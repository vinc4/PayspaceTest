using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PaySpace.Calculator.API.Security
{
    public class DummyTokenAuthorizationPolicy
    {
        public const string PolicyName = "DummyTokenPolicy";

        public class DummyTokenAuthorizationFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                // Retrieve the dummy token from configuration or hardcode it
                string dummyToken = "dummytoken123";

                // Check if the request contains the dummy token
                if (!context.HttpContext.Request.Headers.TryGetValue("Dummy-Token", out var token) || token != dummyToken)
                {
                    // Return unauthorized response if token is missing or invalid
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
