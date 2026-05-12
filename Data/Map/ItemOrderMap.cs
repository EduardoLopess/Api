using Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Map
{
    public class ItemOrderMap : IEntityTypeConfiguration<ItemOrder>
    {
        public void Configure(EntityTypeBuilder<ItemOrder> builder)
        {
            builder.ToTable("ItemPedido");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                .HasColumnName("ProdutoId")
                .IsRequired();

            builder.Property(x => x.Quantity)
                .HasColumnName("Quantidade")
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasColumnName("PrecoUnitario")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Discount)
                .HasColumnName("Desconto")
                .HasPrecision(18, 2)
                .IsRequired();

      
        }
    }
}




