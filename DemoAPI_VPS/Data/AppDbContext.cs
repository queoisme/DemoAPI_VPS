using DemoAPI_VPS.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI_VPS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Student> Students => Set<Student>();
    }
}
