using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Task CreateAsync(Entity entity);
        Task<Entity?> GetByIdAsync(int entityId);
        Task<IList<Entity>> GetAllAsync();
        Task SaveChangesAsync();
        Task UpdateAsync(Entity entity);
    }
}
