using Domain.Common;
using Domain.Employee.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Employee
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<bool> EmailAlreadyRegistered(Email email);
        Task<Employee?> GetByEmailAsync(Email email);
        Task DeleteAsync(Employee employee);
        

    

    }
}
