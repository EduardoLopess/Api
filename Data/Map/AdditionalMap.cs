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
    public class AdditionalMap : IEntityTypeConfiguration<Additional>
    {
        public void Configure(EntityTypeBuilder<Additional> builder)
        {
            builder.ToTable("Adicionais");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(80)")
                .IsRequired();

            builder.Property(x => x.Availability).HasConversion(
              availability => availability.Value.ToString(),
              value => new Availability((StatusAvailability)
              Enum.Parse(typeof(StatusAvailability),
              value)))
              .HasColumnType("varchar")
              .HasMaxLength(50);

            builder.Property(x => x.Price)
                    .HasConversion(
                         price => price != null ? price.Value : (decimal?)null,
                         value => value.HasValue ? new Price(value.Value) : null
                    )
                    .HasPrecision(18, 2)
                    .IsRequired(false);

            builder.Property(x => x.Stock)
                    .HasConversion(
                        stock => stock.Value,
                        value => new QuantityStock(value)
                    ).HasColumnName("QuantideEstoque").IsRequired();
        }


    }
}
