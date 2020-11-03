using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DotNetECoremmerce.ProductCatalogue.API.Auth
{
    public class ViewProductsHandler : AuthorizationHandler<ViewProductsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewProductsRequirement requirement)
        {
            if (context.User.HasClaim("Permissions", "view:products"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }

    }
}