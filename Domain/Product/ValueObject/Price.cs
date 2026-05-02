using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product.ValueObject
{
    public record class Price
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Preço inválido.");

            Value = value;
        }
    }
}
