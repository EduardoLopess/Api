using Domain.Product;
using Domain.Table;
using Domain.Order;
using Microsoft.EntityFrameworkCore;
using Domain.Table.ValueObject;

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

                builder.Property(t => t.Number)
                    .HasColumnName("NumeroMesa").HasMaxLength(2)
                    .IsRequired();

                builder.Property(t => t.Status)
                    .HasConversion<int>();

                builder.Property(t => t.OrderId)
                    .HasConversion(
                        i => i.Value,
                        value => new OrderId(value)
                    );

                builder.OwnsOne(x => x.StatusAccess, sa =>
                {
                    sa.Property(x => x.UserId)
                        .HasConversion(
                            id => id.Value,
                            value => new UserId(value)
                        );

                    sa.Property(x => x.StatusLocked)
                        .HasConversion<int>();
                });

            });

            modelBuilder.Entity<Product>(builder =>
            {


            });

        

        } 
    }
}
