using Domain.Product.Enum;
using Domain.Product.ValueObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product
{
    public  class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public ProductCategory Category { get; private set; }
        public int? CodeNCM { get; private set;  }
        public QuantityStock Stock { get; private set; }
        public Availability Availability { get; private set; }

        private readonly List<Additional> _additional = [];
        public IReadOnlyCollection<Additional>? Additionals => _additional;

        private readonly List<Flavor> _flavor = [];
        public IReadOnlyCollection<Flavor>? Flavors => _flavor;


        public Product(string name, QuantityStock stock , ProductCategory category, Price price, IEnumerable<Flavor>? flavors = null, IEnumerable<Additional>? additionals = null)
        {

            EnsureValidName(name);
            EnsureValidStock(stock);
            EnsureValidCategory(category);
            EnsureValidPrice(price);

            if (flavors is not null)
            {
                foreach (var item in flavors)
                {
                    AddFlavor(item);
                }
            }

            if (additionals is not null)
            {
                foreach (var item in additionals)
                {
                    AddAdditional(item);
                }
            }

            Name = name;
            Price = price;
            Stock = stock;
            Availability.Available();
            Category = category;

        }

    
        private void AddAdditional(Additional additional)
        {
            if (additional is null) 
                throw new ArgumentNullException(nameof(additional), "Adicional não informado");

            if (_additional.Any(a => a.Id == additional.Id)) 
                throw new InvalidOperationException("Adicional já cadastrado no produto.");

            if (!additional.Availability.IsAvailable()) 
                throw new InvalidOperationException("Adicional indisponível.");

            _additional.Add(additional);
        }

        private void AddFlavor(Flavor flavor)
        {
            if (flavor is null) 
                throw new ArgumentNullException(nameof(flavor), "Sabo não informado");

            if (_flavor.Any(f => f.Id == flavor.Id)) 
                throw new InvalidOperationException("Sabor já cadastrado no produto.");

            if (!flavor.Availability.IsAvailable()) 
                throw new InvalidOperationException("Sabor indisponível");

            _flavor.Add(flavor);
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


        private void EnsureValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório.", nameof(name));
        }

        private void EnsureValidStock(QuantityStock stock)
        {
            if (stock is null)
                throw new ArgumentNullException(nameof(stock), "Valor de estoque deve ser informado.");
        }

        private void EnsureValidCategory(ProductCategory category)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category), "Categoria não informada.");
        }

        private void EnsureValidPrice(Price price)
        {
            if (price is null)
                throw new ArgumentNullException(nameof(price), "Preço não informado.");
        }

    }
}
