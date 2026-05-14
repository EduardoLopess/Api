using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Table.ValueObject
{
    public record OrderId
    {
        public Guid Value { get; }
        
        protected OrderId() {}

        public OrderId (Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Id Inválido.");

         
            Value = value;
        }


    }
}
