using Microsoft.EntityFrameworkCore;
using Vidly.Models.Domain;

namespace Vidly.Data
{
    public class VidlyDbContext : DbContext
    {
        public VidlyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
