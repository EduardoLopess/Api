using Application.Service.password;
using Domain.Employee;
using Domain.Employee.ValueObject;

namespace Application.UseCase.EmployeeUseCase.RegisterUseCase
{
    public class RegisterEmployeeUseCase(IEmployeeRepository employeeRepository, IPasswordService passwordService)
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPasswordService _passwordService = passwordService;

        public async Task<Result<RegisterEmployeeResponse>> RegisterUseCase(RegisterEmployeeRequest request)
        {

            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
                return Result<RegisterEmployeeResponse>.Failure(emailResult.Message);

            var passwordResult = Employee.CheckPasswordIsValid(request.Password, request.ConfirmPassword);
            if (passwordResult.IsFailure)
                return Result<RegisterEmployeeResponse>.Failure(passwordResult.Message);


            var emailExists = await _employeeRepository.EmailAlreadyRegistered(emailResult.Value!);
            if (emailExists)
                return Result<RegisterEmployeeResponse>.Failure("Email já cadastrado.");

            var passwordHash = _passwordService.GenerateHash(request.Password);

            var employeeResult = Employee.Create(request.Name, emailResult.Value!, passwordHash, request.RoleAccess);
            if (employeeResult.IsFailure)
                return Result<RegisterEmployeeResponse>.Failure(employeeResult.Message);

            var employee = employeeResult.Value!;

            await _employeeRepository.CreateAsync(employee);

            RegisterEmployeeResponse employeeRegisterResponse = new(employee.Id, employee.Name);

            return Result<RegisterEmployeeResponse>.Success(employeeRegisterResponse, "Funcionário cadastrado com sucesso.");
        }
    }
}
