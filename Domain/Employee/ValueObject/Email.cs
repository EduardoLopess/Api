using Domain.Common;
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

        public static Result<Email> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<Email>.Failure("Email não informado");

            value = value.Trim();

            if (!EmailRegex.IsMatch(value))
                return Result<Email>.Failure("Email com formato inválido.");


            var email = new Email(value);

            return Result<Email>.Success(email, "Email criado com sucesso.");
        }

       
        public override string ToString() => Value;
    }
}