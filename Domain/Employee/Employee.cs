using Domain.Employee.Enum;
using Domain.Employee.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Employee
{
    public class Employee
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public RoleAccess RoleAccess { get; private set; }

        protected Employee() {}

        public Employee ( string name, Email email, string passwordHash, RoleAccess roleAccess)
        {
            EnsureValidName(name);
            EnsureValidPasswordHash(passwordHash);
            EnsureValidRoleAcess(roleAccess);

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            RoleAccess = roleAccess;
        }

        public void UpdatePassword (string newPasswordHash)
        {
            if (string.IsNullOrEmpty(newPasswordHash)) 
                throw new ArgumentNullException(nameof(newPasswordHash));

            PasswordHash = newPasswordHash;
        }

        public void ChangeRoleAccess(RoleAccess roleAccess)
        {
            if (roleAccess == RoleAccess)
                throw new InvalidOperationException("Funcionário já possui essa Role");

            EnsureValidRoleAcess(roleAccess);

            RoleAccess = roleAccess;
        }

        public void UpdateEmail (string email)
        {
            Email = Email.Create(email);

        }

        private void EnsureValidName (string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome não informado.");

            if (!Regex.IsMatch(name, @"^[\p{L}]+(?:\s[\p{L}]+)*$"))
                throw new ArgumentException("Nome deve conter apenas letras.");
        }

        private void EnsureValidPasswordHash (string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Hash da senha não informado.");
        }

        private void EnsureValidRoleAcess (RoleAccess roleAccess)
        {
            if (roleAccess == RoleAccess.None)
                throw new ArgumentException("Permissão inválida.");
        }
     
    }
}
