using Microsoft.AspNetCore.Authorization;
using Supabase;
using System.Security.Claims;
using WiseMaestroRBAC.Models;
using WiseMaestroRBAC.Controllers;
using WiseMaestroRBAC.Services;

namespace WiseMaestroRBAC.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly Client _supabaseClient;

        public RoleAuthorizationHandler(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RoleRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            try
            {
                // Query your users table directly using your UserModel
                var response = await _supabaseClient
                    .From<UserModel>()
                    .Where(u => u.Id == userId)
                    .Single();

                if (response != null && !string.IsNullOrEmpty(response.Role))
                {
                    // Compare role levels
                    int userRoleLevel = UserRoles.GetRoleLevel(response.Role);
                    int requiredRoleLevel = UserRoles.GetRoleLevel(requirement.RequiredRole);

                    if (userRoleLevel >= requiredRoleLevel)
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            catch (Exception)
            {
                // Handle any errors appropriately
                return;
            }
        }
    }

    public class RoleRequirement : IAuthorizationRequirement
    {
        public string RequiredRole { get; }

        public RoleRequirement(string requiredRole)
        {
            RequiredRole = requiredRole;
        }
    }

    // 4. Updated attribute for controllers/actions
    public class RequireRoleAttribute : AuthorizeAttribute
    {
        public RequireRoleAttribute(string requiredRole)
        {
            Policy = $"RequireRole{requiredRole}";
        }
    }
}
