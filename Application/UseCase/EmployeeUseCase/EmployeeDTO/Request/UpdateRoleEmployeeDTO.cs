using Domain.Employee.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.EmployeeDTO.Request
{
    public record UpdateRoleEmployeeDTO
    (string Id, RoleAccess Role);
}
