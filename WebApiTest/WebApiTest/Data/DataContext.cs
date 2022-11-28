using Microsoft.EntityFrameworkCore;
using WebApiTest.Model;

namespace WebApiTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ):base(options) { }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
    }
}
