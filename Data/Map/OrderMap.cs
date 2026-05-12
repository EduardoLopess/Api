using Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Data.Map
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.DateCreate)
                .HasColumnName("DataCriacao")
                .IsRequired();

            builder.Property(o => o.DataCompletion)
                .HasColumnName("DataFinalizacao")
                .IsRequired();

            builder.Property(o => o.TableId)
                .HasColumnName("MesaId")
                .IsRequired();
                      
            builder.HasMany(o => o.OrderItens)
                 .WithOne()
                 .HasForeignKey("OrderId");

            builder.Navigation(o => o.OrderItens)
                 .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(o => o.TotalOrder)
                .HasColumnName("TotalPedido")
                .HasPrecision(18, 2)
                .IsRequired();

        }
    }
}
