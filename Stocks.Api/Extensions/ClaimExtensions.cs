using System.Security.Claims;

namespace Stocks.Api.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetUserName(this ClaimsPrincipal User)
        {
            return User.FindFirstValue(ClaimTypes.GivenName);
        }

        public static string GetUserId(this ClaimsPrincipal User)
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
