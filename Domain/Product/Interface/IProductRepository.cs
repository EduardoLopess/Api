using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product.Interface
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<bool> NameIsAlreadyRegistered(string name);
    }
}
