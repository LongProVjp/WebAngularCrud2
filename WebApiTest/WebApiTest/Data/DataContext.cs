using Microsoft.EntityFrameworkCore;
using WebApiTest.Model;

namespace WebApiTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ):base(options) { }
        public DbSet<ModelProvince> Provinces { get; set; }
        public DbSet<ModelDistrict> Districts { get; set; }
    }
}
