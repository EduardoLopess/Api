using Domain.Employee.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.RegisterUseCase
{
    public record RegisterEmployeeRequest
    (
        string Name,
        string Password,
        string Email,
        string ConfirmPassword,
        RoleAccess RoleAccess
    );
}
