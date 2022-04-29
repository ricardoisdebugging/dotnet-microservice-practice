using Microsoft.EntityFrameworkCore;
using PlatformService.PlatformDomain;

namespace PlatformService.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
            :base(opt)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
