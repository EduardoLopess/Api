using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.LoginUseCase
{
    public record LoginEmployeeRequest
    (string Email, string Password);
}
