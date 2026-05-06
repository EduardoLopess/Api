using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product.ValueObject
{
    public class QuantityStock
    {
        public int Value { get; }

        public QuantityStock(int value)
        {

            if (value < 0) 
                throw new ArgumentException("Estoque não pode ser negativo", nameof(value));

            Value = value;
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
