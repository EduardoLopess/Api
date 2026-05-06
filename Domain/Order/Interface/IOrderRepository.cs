using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Order.Interface
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        bool HasAnyOrderWithProduct(int productId);
    }
}
