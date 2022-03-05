using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestIdentity.DTO
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=45.34.14.205;userid=madushan;password=800540186;database=TestUSerID;persistsecurityinfo=True;convert zero datetime=True;SslMode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.14-mariadb"));
            }
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
          : base(options)
        {

        }
    }
}
