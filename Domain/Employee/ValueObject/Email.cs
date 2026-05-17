using System.Text.RegularExpressions;

namespace Domain.Employee.ValueObject
{
    public record Email
    {
        private static readonly Regex EmailRegex = new (
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        public string Value { get; }

        private Email(string value) => Value = value;

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não informado.");

           
            email = email.Trim();

            if (!EmailRegex.IsMatch(email))
                throw new ArgumentException("Email com formato inválido.");

          
            return new Email(email);
        }

       
        public override string ToString() => Value;
    }
}