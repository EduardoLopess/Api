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
        public Price Price { get; private set; }
        public Category Category { get; private set; }
        public int CodeNCM { get; private set;  }

        private readonly List<Additional> _additional = [];
        public IReadOnlyCollection<Additional>? Additionals => _additional;

        private readonly List<Flavor> _flavor = [];
        public IReadOnlyCollection<Flavor>? Flavors => _flavor;


        public Product(IEnumerable<Flavor>? flavors = null, IEnumerable<Additional>? additionals = null)
        {
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

        }



        private void AddAdditional(Additional additional)
        {
            if (additional is null) throw new ArgumentNullException(nameof(additional));

            if (_additional.Contains(additional)) throw new InvalidOperationException("Adicional já cadastrado no produto.");

            if (!additional.IsAvailable()) throw new InvalidOperationException("Adicional indisponível.");

            _additional.Add(additional);
        }

        private void AddFlavor(Flavor flavor)
        {
            if (flavor is null) throw new ArgumentNullException(nameof(flavor));

            if (_flavor.Contains(flavor)) throw new InvalidOperationException("Sabor já cadastrado no produto.");

            if (!flavor.IsAvailable()) throw new InvalidOperationException("Sabor indisponível");

            _flavor.Add(flavor);
        }

    }
}
