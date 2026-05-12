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
    public class FlavorMap : IEntityTypeConfiguration<Flavor>
    {
        public void Configure(EntityTypeBuilder<Flavor> builder)
        {
            builder.ToTable("Sabores");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("Nome")
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
        }
}


public int Id { get; private set; }
public string Name { get; private set; } = string.Empty;
public Availability Availability { get; private set; }
public Price? Price { get; private set; }
public QuantityStock Stock { get; private set; }
