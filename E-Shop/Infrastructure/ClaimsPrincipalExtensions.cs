namespace E_Shop.Infrastructure
{
    using System.Security.Claims;
    using static WebConstants;
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
