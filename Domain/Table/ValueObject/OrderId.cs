using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Table.ValueObject
{
    public record OrderId
    {
        public int Value { get; }
        
        protected OrderId() {}

        public OrderId (int value)
        {
            if (value <= 0)
                throw new ArgumentException("Id Inválido.");

         
            Value = value;
        }


    }
}
