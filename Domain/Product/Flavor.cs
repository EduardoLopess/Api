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
        public QuantityStock Stock { get; private set; }


        protected Flavor() { }

        public Flavor(string name, Availability availability)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Nome do sabor deve ser informado.");

            Name = name;
            Availability = availability;


        }

        public void AddStock(QuantityStock quantityToAdd)
        {
            if (quantityToAdd is null)
                throw new ArgumentNullException(nameof(quantityToAdd), "Valor estoque não informado.");

            Stock = Stock.AddStock(quantityToAdd);

            if (Stock.HasIsStock() && !Availability.IsAvailable())
            {
                Available();
            }
        }

        public void DebitStock(QuantityStock quantityToDebit)
        {
            if (quantityToDebit is null) 
                throw new ArgumentNullException(nameof(quantityToDebit), "Valor a ser decrementado não informado.");

            Stock = Stock.DebitStock(quantityToDebit);

            if (Stock.StockIsEmpty() && Availability.IsAvailable())
            {
                Unavailable();
            }

        }

        private void Unavailable ()
        {
            if (!Availability.IsAvailable()) 
                throw new InvalidOperationException("Produto já indisponível");

            Availability.Unavailable();
        }

        private void Available()
        {
            if (Availability.IsAvailable()) 
                throw new InvalidOperationException("Produto já disponível.");

           Availability.Available();
        }

    }
}
