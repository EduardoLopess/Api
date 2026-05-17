using Data.Context;
using Domain.Employee;
using Domain.Employee.ValueObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class EmployeeRepository(DataContext context) : IEmployeeRepository
    {

        private readonly DataContext _context = context;

        public Task CreateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EmailAlreadyRegistered(Email email)
        {
            return await _context.Employees
                .AnyAsync(e => e.Email.Value == email.Value);
        }
    }
}
