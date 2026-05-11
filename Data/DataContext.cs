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
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemOrder> Items { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

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
