using Microsoft.AspNetCore.Authorization;

namespace PaySpace.Calculator.API.Security
{
    public class DummyTokenAuthorizationRequirement : IAuthorizationRequirement
    {
        // This class  left empty since it's just a marker interface
    }
}
