using Data.Context;
using Domain.Product.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class CategoryRepository(DataContext context) : ICategoryRepository
    {
        private readonly DataContext _context = context;

        public Task CreateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetGuidsAsync(List<Guid> ids)
        {
            return await _context.Categories
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
