using Domain.Employee;
using Domain.Employee.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Map
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Funcionarios");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("NomeFuncionario")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Email).HasConversion(
                email => email.Value,
                value => Email.Create(value))
                .HasColumnName("Email")
                .IsRequired();



            builder.Property(x => x.RoleAccess)
                .HasColumnName("NivelAcesso")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasColumnName("Senha")
                .HasColumnType("varchar")
                .IsRequired();

                
        }
    }
}


