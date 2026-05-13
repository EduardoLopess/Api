using Domain.Product;
using Domain.Product.Enum;
using Domain.Product.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Data.Map
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnName("NomeProduto")
                .HasColumnType("varchar(80)")
                .IsRequired();

            builder.Property(p => p.Price).HasConversion(
                price => price.Value,
                value => new Price(value)).HasPrecision(18, 2).IsRequired();

            builder.OwnsOne(p => p.Category, vo =>
            {
                vo.Property(p => p.Category)
                    .HasColumnName("Categoria")
                    .IsRequired();

                vo.Property(p => p.Type)
                    .HasColumnName("Tipo")
                    .IsRequired();

                vo.Property(p => p.SubType)
                    .HasColumnName("SubTipo")
                    .IsRequired();

            });

            builder.Property(x => x.Stock)
                   .HasConversion(
                       stock => stock.Value,
                       value => new QuantityStock(value)
                   ).HasColumnName("QuantideEstoque").IsRequired();

            builder.Property(p => p.Availability).HasConversion(
                availability => availability.Value.ToString(),
                value => new Availability((StatusAvailability)
                Enum.Parse(typeof(StatusAvailability), 
                value)))
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.HasMany(p => p.Flavors)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "ProdutoSabor",

                    joinBuilder => joinBuilder
                        .HasOne<Flavor>()
                        .WithMany()
                        .HasForeignKey("FlavorId"),

                    joinBuilder => joinBuilder
                        .HasOne<Product>()
                        .WithMany()
                        .HasForeignKey("ProductId")
                );

            builder.HasMany(p => p.Additionals)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "ProdutoAdc",

                    joinBuilder => joinBuilder
                        .HasOne<Additional>()
                        .WithMany()
                        .HasForeignKey("AdditionalId"),

                    joinBuilder => joinBuilder
                        .HasOne<Product>()
                        .WithMany()
                        .HasForeignKey("ProductId")
              
                );

                   

        }
    }
}
