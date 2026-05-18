using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.EmployeeDTO.Request
{
    public record UpdateEmailEmployeeDTO
    (
        string Id,
        string Email
    );
}
