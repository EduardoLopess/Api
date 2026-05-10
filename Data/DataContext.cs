using Domain.Product;
using Domain.Table;
using Domain.Order;
using Microsoft.EntityFrameworkCore;
using Domain.Table.ValueObject;
using Domain.Product.ValueObject;

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
                builder.ToTable("Produtos");
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Name)
                    .HasColumnName("NomeProduto")
                    .HasColumnType("varchar(50)")
                    .IsRequired();

                builder.OwnsOne(p => p.Price, vo =>
                {
                    vo.Property(x => x.Value)
                        .HasColumnName("Price")
                        .HasPrecision(18, 2);
                });



            });


            modelBuilder.Entity<Category>(builder =>
            {
                builder.ToTable("Categoria");
                builder.HasKey(c => c.Id);

                builder.Property(c => c.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.HasOne(c => c.Parent)
                    .WithMany(c => c.Children)
                    .HasForeignKey(c => c.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

        

        } 
    }
}
