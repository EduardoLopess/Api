using Domain.Common;


namespace Domain.Table
{
    public interface ITableRepository : IBaseRepository<Table> { Task Delete(Table table); }
    
}
