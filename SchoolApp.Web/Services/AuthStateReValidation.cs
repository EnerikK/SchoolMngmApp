using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace SchoolApp.Web.Services
{
    public class AuthStateReValidation(ILoggerFactory loggerFactory,
        IServiceScopeFactory serviceScopeFactory,
        IOptions<IdentityOptions> options) :  RevalidatingServerAuthenticationStateProvider (loggerFactory)
    {

        //Time in which the server should revalidate this state
        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(20); //RevalidateEvery20Minutes

        protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            return await ValidateSecurityTimeStampAsync(userManager,authenticationState.User);
        }

        private async Task<bool> ValidateSecurityTimeStampAsync(UserManager<IdentityUser> userManager , ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if(user is null) return false;

            var principalStamp = principal.FindFirstValue(options.Value.ClaimsIdentity.SecurityStampClaimType);
            var userStamp = await userManager.GetSecurityStampAsync(user);

            return principalStamp == userStamp;
        }
    }
}
