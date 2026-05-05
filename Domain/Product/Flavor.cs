using Domain.Product.Enum;
using Domain.Product.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product
{
    public class Flavor
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Availability Availability { get; private set; }
        public Price? Price { get; private set; }
        

        public Flavor (string name)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Nome do sabor deve ser informado.");

        }
       
    }
}
