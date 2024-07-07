using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Web.Components.Data
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions opt) : base(opt) 
        {

        }
    }
}
