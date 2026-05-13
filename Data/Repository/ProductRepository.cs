using Domain.Product;
using Domain.Product.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetByIdAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
