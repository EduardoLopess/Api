using Domain.Order;
using Domain.Order.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class ItemOrderRepository : IItemOrderRepository
    {
        public Task CreateAsync(ItemOrder entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ItemOrder>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemOrder?> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ItemOrder entity)
        {
            throw new NotImplementedException();
        }
    }
}
