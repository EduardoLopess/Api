using Domain.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class TableRepository : ITableRepository
    {
        public Task CreateAsync(Table entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Table table)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Table>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Table?> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Table entity)
        {
            throw new NotImplementedException();
        }
    }
}
 