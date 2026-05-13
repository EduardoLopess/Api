using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Map
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasColumnType("varchar(80)")
                .IsRequired();

            
            builder.HasOne(c => c.Parent)         
                .WithMany(c => c.Children)        
                .HasForeignKey(c => c.ParentId)   
                .OnDelete(DeleteBehavior.Restrict);

          
            builder.Navigation(c => c.Children)
                .HasField("_children")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
