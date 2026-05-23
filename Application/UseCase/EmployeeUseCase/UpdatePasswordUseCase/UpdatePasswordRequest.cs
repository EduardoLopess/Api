using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.UpdatePasswordUseCase
{
    public record UpdatePasswordRequest
    (string Email, string Password, string ConfirmPassword);
}
