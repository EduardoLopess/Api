using Domain.Product;
using Domain.Product.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class AdditionalRepository : IAdditionalRepository
    {
        public Task CreateAsync(Additional entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Additional>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Additional?> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Additional entity)
        {
            throw new NotImplementedException();
        }
    }
}
