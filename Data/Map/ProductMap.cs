using Domain.Product;
using Domain.Product.Enum;
using Domain.Product.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

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
                value => new Price(value));

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

            builder.OwnsOne(p => p.Availability, vo =>
            {
                vo.Property(p => p.Value)
                    .HasColumnName("Disponibilidade")
                    .IsRequired()
                    .HasConversion<int>();

            });

            builder.Property(p => p.Availability).HasConversion(
                availability => availability.Value.ToString(),
                value => new Availability((StatusAvailability)Enum.Parse(typeof(StatusAvailability), 
                value))
                )
                .HasColumnType("varchar")
                .HasMaxLength(50);

          
                

         



            
        }
    }
}
