using System.Reflection.Metadata;
using System.Security.Claims;
using JediChat.Server.Configuration;

namespace JediChat.Server.Extensions
{
    public static class GetUserIdExtension
    {
        public static string GetUserId(this ClaimsPrincipal user, string userIdClaimType = Constants.UserIdClaimTypeName)
        {
            return user.FindFirst(userIdClaimType)?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal user, string userNameClaimType = Constants.UserNameClaimTypeName)
        {
            return user.FindFirst(userNameClaimType)?.Value;
        }
    }
}