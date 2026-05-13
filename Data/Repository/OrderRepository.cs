using Domain.Order;
using Domain.Order.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public Task CreateAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetByIdAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public bool HasAnyOrderWithProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
