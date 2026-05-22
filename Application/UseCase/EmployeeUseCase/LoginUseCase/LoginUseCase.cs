using Application.Service.password;
using Domain.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.LoginUseCase
{
    public class LoginUseCase(IEmployeeRepository employeeRepository, IPasswordService passwordService)
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPasswordService _passwordService = passwordService;

        public Task<Result<LoginEmployeeResponse>> LoginUseCase(LoginEmployeeRequest request)
        {
            return Result<LoginEmployeeResponse>.Success("Login efetuado com sucesso");
        }
    }
}
