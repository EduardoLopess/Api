using Domain.Table;
using Domain.Table.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Map
{
    public class TableMap : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Mesas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Number)
                .HasColumnName("NumeroMesa")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(t => t.Status)
                .HasConversion<int>();

            builder.Property(t => t.OrderId)
                .HasConversion(
                    id => id.Value,
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
        }
    }
}