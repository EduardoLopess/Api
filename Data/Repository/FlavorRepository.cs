using Domain.Product;
using Domain.Product.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class FlavorRepository : IFlavorRepository
    {
        public Task CreateAsync(Flavor entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Flavor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Flavor?> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Flavor entity)
        {
            throw new NotImplementedException();
        }
    }
}
