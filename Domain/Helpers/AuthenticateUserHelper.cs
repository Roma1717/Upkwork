using System.Security.Claims;
using Domain.ModelsDb;
namespace Domain.Helpers;

public static class AuthenticateUserHelper
{
    public static ClaimsIdentity Authenticate(UserDb user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
      
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);
    }
}