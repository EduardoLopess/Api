using Domain.Product;
using Domain.Table;
using Domain.Order;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Table> Tables { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<Additional> Additionals { get; set; }
        public DbSet<Order> Orders { get; set;  }
        public DbSet<ItemOrder> Items { get; set; }


      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>(builder =>
            {
                builder.ToTable("Mesas");
                builder.HasKey(t => t.Id);

                builder.Property(t => t.Number).HasColumnName("NumeroMesa").IsRequired();

                //builder.OwnsOne(l => l.LockedAcess, vo =>
              //  {
                    
            //   // })
          // })


        }
    }
}
