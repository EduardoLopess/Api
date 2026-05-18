using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.EmployeeDTO.Response
{
    public record GetByIdEmployeeDTO
    (
        string Id,
        string Name,
        string Role,
        string Email
    );
    
}
