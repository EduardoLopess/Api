using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.EmployeeDTO.Request
{
    public record LoginEmployeeRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
