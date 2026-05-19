using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product.ValueObject
{
    public class QuantityStock
    {
        public int Value { get; }

        private QuantityStock(int value)
        {

            Value = value;
        }

        public static Result<QuantityStock> Create (int value)
        {
            if (value < 0)
                return Result<QuantityStock>.Failure("Quantidade de estoque inicial não pode ser menor que 0");

            var quantityStock = new QuantityStock(value);

            return Result<QuantityStock>.Success(quantityStock, "Estoque criado com sucesso.");
        }

        public QuantityStock AddStock (QuantityStock newValue)
        {
            if (newValue.Value < 0) 
                throw new ArgumentException("Valor inválido.");

            return new QuantityStock(Value + newValue.Value);
        }

        public QuantityStock DebitStock (QuantityStock newValue)
        {
            if (newValue.Value < 0 || newValue.Value > Value) 
                throw new InvalidOperationException("Estoque inválido");

            return new QuantityStock(Value - newValue.Value);
        }

        public bool StockIsEmpty () => Value == 0;
        public bool HasIsStock() => Value > 0;
    }
}
