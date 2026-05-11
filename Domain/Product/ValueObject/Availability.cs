using Domain.Product.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product.ValueObject
{
    public class Availability
    {
        public StatusAvailability Value { get; private set; }

        public Availability(StatusAvailability status)
        {
            Value = status;
        }


        public bool IsAvailable() => Value == StatusAvailability.Disponível;
        public void Unavailable() => Value = StatusAvailability.Indisponível;
        public void Available() => Value = StatusAvailability.Disponível;
    }
}
