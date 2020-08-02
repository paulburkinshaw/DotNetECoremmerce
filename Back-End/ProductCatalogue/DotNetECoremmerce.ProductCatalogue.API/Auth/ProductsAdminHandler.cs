using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DotNetECoremmerce.ProductCatalogue.API.Auth
{
    public class ProductsAdminHandler : AuthorizationHandler<ProductsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProductsAdminRequirement requirement)
        {
            if (context.User.HasClaim("Permissions", "edit:products"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }

    }
}