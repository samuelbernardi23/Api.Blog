using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TopCon.Api.Entities;

namespace TopCon.Api.Data
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        { }

        public DbSet<Posts> Posts {get; set;}
    }
}
