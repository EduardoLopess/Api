using Domain.Product.Enum;
using Domain.Product.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product
{
    public class Additional
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Availability Availability { get; private set; }
        public Price Price { get; private set;  }

        public bool IsAvailable() => Availability == Availability.Disponível;
    }
}
