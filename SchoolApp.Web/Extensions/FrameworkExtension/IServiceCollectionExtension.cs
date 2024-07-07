using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SchoolApp.Web.Services;

namespace SchoolApp.Web.Extensions.FrameworkExtension
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddCascadingAuthenticationState();
            services.AddScoped<AuthenticationStateProvider, AuthStateReValidation>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            }).AddIdentityCookies();

            services.AddScoped<CookieEvents>();
            services.ConfigureApplicationCookie(options =>
            {
                options.EventsType = typeof(CookieEvents);
            });

            return services;
        }

    }
}
