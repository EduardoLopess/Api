using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product.ValueObject
{
    public record class Price
    {
        public decimal Value { get; }

        private Price(decimal value)
        {
            Value = value;
        }

        public static Result<Price> Create(decimal value)
        {
            if (value <= 0)
                return Result<Price>.Failure("Preço do produto inválido.");

            var price = new Price(value);
            
            return Result<Price>.Success(price, "Preço criado com sucesso.");
        }
    }
}
