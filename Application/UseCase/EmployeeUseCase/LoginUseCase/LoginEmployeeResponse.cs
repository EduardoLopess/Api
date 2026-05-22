using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase.LoginUseCase
{
    public record LoginEmployeeResponse
    (Guid Id, string Name, string Token);
}
