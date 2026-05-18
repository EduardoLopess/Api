using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.EmployeeDTO.Response
{
    public record LoginResponseDTO
    (
         string Id,
         string Name,
         string Role,
         string Token

    );
}
