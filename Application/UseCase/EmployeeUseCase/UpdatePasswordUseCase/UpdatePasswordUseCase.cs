using Application.Service.password;
using Domain.Employee;
using Domain.Employee.ValueObject;


namespace Application.UseCase.EmployeeUseCase.UpdatePasswordUseCase
{
    public class UpdatePasswordUseCase(IEmployeeRepository employeeRepository, IPasswordService passwordService)
    {
        private IEmployeeRepository _employeeRepository = employeeRepository;
        private IPasswordService _passwordService = passwordService;

        public async Task<Result<UpdatePasswordResponse>> UpdatePassword(UpdatePasswordRequest request)
        {
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
                return Result<UpdatePasswordResponse>.Failure(emailResult.Message);

            var validPassword = Employee.CheckPasswordIsValid(request.Password, request.ConfirmPassword);
            if (validPassword.IsFailure)
                return Result<UpdatePasswordResponse>.Failure(emailResult.Message);

            var employee = await _employeeRepository.GetByEmailAsync(emailResult.Value!);
            if (employee is null)
                return Result<UpdatePasswordResponse>.Failure("Funcionário não foi encontrado.");

            var newPassword = _passwordService.GenerateHash(request.Password);

            employee.UpdatePassword(newPassword);

            await _employeeRepository.SaveChangesAsync();

            UpdatePasswordResponse updatePasswordResponse = new(employee.Name);

            return Result<UpdatePasswordResponse>.Success(updatePasswordResponse, $"Senha de {updatePasswordResponse.Name} atualizada com sucesso.");
        }
    }
}
