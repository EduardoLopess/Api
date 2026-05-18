using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.EmployeeDTO.Response
{
    public record ListEmployeeDTO
    (
        string Id,
        string Nome,
        string Role
    );
}
