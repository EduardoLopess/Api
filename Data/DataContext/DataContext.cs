using Domain.Product;
using Domain.Table;
using Domain.Order;
using Microsoft.EntityFrameworkCore;


namespace Data.DataContext
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Table> Tables { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<Additional> Additionals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemOrder> Items { get; set; }
        public DbSet<Category> Categories {get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
