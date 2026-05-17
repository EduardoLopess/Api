using Domain.Employee.Enum;


namespace Application.UseCase.EmployeeUseCase.EmployeeDTO.Request
{
    public record RegisterEmployeeRequestDTO(
    string Name,
    string Password,
    string Email,
    string ConfirmPassword,
    RoleAccess RoleAccess
    );
}
