using Domain.Product.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product.ValueObject
{
    public class Availability
    {
        public StatusAvailability Value { get; private set; }

        public Availability()
        {
            Value = StatusAvailability.Disponível;
        }


        public bool IsAvailable() => Value == StatusAvailability.Disponível;
        public void Unavailable() => Value = StatusAvailability.Indisponível;
        public void Available() => Value = StatusAvailability.Disponível;
    }
}
