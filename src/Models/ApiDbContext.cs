using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Models
{
    public class ApiDbContext : DbContext
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserItem> Users { get; set; } = null!;
    }
}
