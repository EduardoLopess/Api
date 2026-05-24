using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.UpdateEmailUseCase
{
    public record UpdateEmailRequest
    (Guid Id, string Email);
}
