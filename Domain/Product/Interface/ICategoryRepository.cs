using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product.Interface
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Guid>> GetGuidsAsync(List<Guid> ids);
    }
}
