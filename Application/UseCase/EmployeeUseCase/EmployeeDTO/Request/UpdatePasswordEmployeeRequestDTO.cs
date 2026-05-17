using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.EmployeeDTO.Request
{
    public record UpdatePasswordEmployeeRequestDTO
    (string Email, string Password);
}
