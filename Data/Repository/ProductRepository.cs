using Data.Context;
using Domain.Product;
using Domain.Product.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class ProductRepository(DataContext context) : IProductRepository
    {
        private readonly DataContext _context = context;
        public Task CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> NameIsAlreadyRegistered(string name)
        {
            return await _context.Products.AnyAsync(x => x.Name == name);
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
