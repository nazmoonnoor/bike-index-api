using Microsoft.EntityFrameworkCore;
using Theft.Core.Domains;

namespace Theft.Core
{
    /// <summary>
    /// Application db context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<City>? Cities { get; set; }
    }
}
