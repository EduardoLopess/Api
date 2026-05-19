using Domain.Common;

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
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public ProductCategory Category { get; private set; }
        public int? CodeNCM { get; private set;  }
        public QuantityStock Stock { get; private set; }
        public Availability Availability { get; private set; }

        private readonly List<Additional> _additional = [];
        public IReadOnlyCollection<Additional> Additionals => _additional;

        private readonly List<Flavor> _flavor = [];
        public IReadOnlyCollection<Flavor> Flavors => _flavor;
        
        protected Product() { }

        public Product(string name, Price price, QuantityStock stock, ProductCategory category, Availability availability, IEnumerable<Flavor>? flavors = null, IEnumerable<Additional>? additionals = null)
        {

            EnsureValidName(name);
            EnsureValidCategory(category);
            

            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Stock = stock;
            Availability = availability;
            Category = category;

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

        public static Result<Product> CreteProductBase(string name, Price price, QuantityStock stock, ProductCategory category, Availability availability)
        {

            var resultName = EnsureValidName(name);

            if (resultName.IsFailure)
                return Result<Product>.Failure(resultName.Message);

            var resultCategory = EnsureValidCategory(category);
            if (resultCategory.IsFailure)
                return Result<Product>.Failure(resultCategory.Message);

            var product = new Product(
                name,
                price,
                stock,
                category,
                availability
            );

            return Result<Product>.Success(product, "Producto criado.");
        }

        
    
        private Result AddAdditional(Additional additional)
        {
            if (additional is null)
                return Result.Failure("Adicional não informado");


            if (_additional.Any(a => a.Id == additional.Id))
                return Result.Failure("Adicional já cadastrado no produto.");

            if (!additional.Availability.IsAvailable())
                return Result.Failure("Adicional indisponível.");

            _additional.Add(additional);

            return Result.Success();
        }

        private Result AddFlavor(Flavor flavor)
        {
            if (flavor is null)
                return Result.Failure("Sabor não informado.");

            if (_flavor.Any(f => f.Id == flavor.Id))
                return Result.Failure("Sabor já cadastrado no produto.");

            if (!flavor.Availability.IsAvailable())
                return Result.Failure("Sabor indiponivel");

            _flavor.Add(flavor);

            return Result.Success();
        }


        public Result AddStock(QuantityStock quantityToAdd)
        {
            if (quantityToAdd is null)
                return Result.Failure("Valor estoque não informado.");

            Stock = Stock.AddStock(quantityToAdd);

            if (Stock.HasIsStock() && !Availability.IsAvailable())
            {
                Available();
            }

            return Result.Success();
        }

        public Result DebitStock(QuantityStock quantityToDebit)
        {
            if (quantityToDebit is null)
                return Result.Failure("Valor a ser decrementado não informado.");

            Stock = Stock.DebitStock(quantityToDebit);

            if (Stock.StockIsEmpty() && Availability.IsAvailable())
            {
                Unavailable();
            }

            return Result.Success();

        }

        private Result Unavailable ()
        {
            if (!Availability.IsAvailable())
                return Result.Failure("Produto já indisponível");

            Availability.Unavailable();

            return Result.Success();
        }

        private Result Available()
        {
            if (Availability.IsAvailable())
                return Result.Failure("Produto já disponível.");

            Availability.Available();
            
            return Result.Success();
        }


        private static Result EnsureValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure("Nome é obrigatório.");
        
            return Result.Success();
        }



        private static Result EnsureValidCategory(ProductCategory category)
        {
            if (category is null)
                return Result.Failure("Categoria não informada.");

            if (category.TypeId == Guid.Empty)
                return Result.Failure("Tipo do produto não informado.");

            if (category.SubTypeId == Guid.Empty)
                return Result.Failure("Subtipo do produto não informado.");

            return Result.Success();
        }

      

    }
}
