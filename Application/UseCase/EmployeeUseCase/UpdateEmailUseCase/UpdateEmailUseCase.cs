using Domain.Employee;
using Domain.Employee.ValueObject;

namespace Application.UseCase.EmployeeUseCase.UpdateEmailUseCase
{
    public class UpdateEmailUseCase(IEmployeeRepository employeeRepository)
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;


        public async Task<Result<UpdateEmailResponse>> Execute(UpdateEmailRequest request)
        {
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
                return Result<UpdateEmailResponse>.Failure(emailResult.Message);

            var emailExists = await _employeeRepository.EmailAlreadyRegistered(emailResult.Value!);
            if (emailExists)
                return Result<UpdateEmailResponse>.Failure("Email já cadastrado.");

            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee is null)
                return Result<UpdateEmailResponse>.Failure("Funcionário não encontrado.");

            employee.UpdateEmail(emailResult.Value!);

            UpdateEmailResponse updateEmailResponse = new(employee.Id, employee.Name);

            await _employeeRepository.SaveChangesAsync();

            return Result<UpdateEmailResponse>.Success(updateEmailResponse, $"Email do funcionário {updateEmailResponse.Name} atualizada com sucesso.");
        }
    }
}
