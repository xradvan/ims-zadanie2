using Microsoft.EntityFrameworkCore;
using zadanie2_webapp.API.Models;

namespace zadanie2_webapp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Value> Values { get; set; }
        public DbSet<TestTable> TestTables { get; set; }
        public DbSet<Novinka> Novinky { get; set; }
        public DbSet<Termin> Terminy { get; set; }
    }
}