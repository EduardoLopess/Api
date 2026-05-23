using Application.Service.password;
using Application.Service.Token;
using Domain.Employee;
using Domain.Employee.ValueObject;

namespace Application.UseCase.EmployeeUseCase.LoginUseCase
{
    public class LoginEmployeeUseCase(IEmployeeRepository employeeRepository, IPasswordService passwordService, ITokenEmployee tokenEmployee)
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ITokenEmployee _tokenEmployee = tokenEmployee;

        public async Task<Result<LoginEmployeeResponse>> LoginUseCase(LoginEmployeeRequest request)
        {

            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
                return Result<LoginEmployeeResponse>.Failure(emailResult.Message);

            var employee = await _employeeRepository.GetByEmailAsync(emailResult.Value!);
            if (employee is null)
                return Result<LoginEmployeeResponse>.Failure("Login inválido.");

            if (!_passwordService.VerifyPassword(request.Password, employee.PasswordHash))
                return Result<LoginEmployeeResponse>.Failure("Login inválido.");

            var employeeToken = new TokenEmployee(employee.Id, employee.Name, employee.Email.ToString(), employee.RoleAccess.ToString());

            var token = _tokenEmployee.GenerateToken(employeeToken);

            LoginEmployeeResponse loginEmployeeResponse = new(employeeToken.Id, employeeToken.Name, token);

            return Result<LoginEmployeeResponse>.Success(loginEmployeeResponse, "Login efetuado com sucesso");
        }
    }
}
