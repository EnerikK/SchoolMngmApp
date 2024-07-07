using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Web.Components.Data;

namespace SchoolApp.Web.Extensions.FrameworkExtension
{
    public static class WebApplicationBuilderExtensions
    {
        //Identity Configuration
        public static WebApplicationBuilder ConfigureIdentity(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContextFactory<AppDBContext>(opt => opt.UseSqlServer(connectionString));
            builder.Services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddIdentityCore<IdentityUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.ClaimsIdentity.UserIdClaimType = "SchoolId";
            }).AddRoles<IdentityRole>().AddSignInManager().AddEntityFrameworkStores<AppDBContext>();
            return builder;
        }
    }
}
