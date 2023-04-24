using Microsoft.EntityFrameworkCore;
using PoseidonApi.Models;

namespace PoseidonApi.Models
{
    public class ApiDbContext : DbContext
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Rule> Rules { get; set; }
    }
}
