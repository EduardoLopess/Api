using Application.DTOs.TableDTO;
using Application.Service;
using Application.Service.Token;
using Application.UseCase.EmployeeUseCase.EmployeeDTO.Request;
using Application.UseCase.EmployeeUseCase.EmployeeDTO.Response;
using Domain.Employee;
using Domain.Employee.ValueObject;


namespace Application.UseCase.EmployeeUseCase
{
    public class EmployeeUseCase(IEmployeeRepository employeeRepository, PasswordService passwordService, TokenService tokenService)
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly PasswordService _passwordService = passwordService;
        private readonly TokenService _tokenService = tokenService;

        private Email CreateVO(string email) => Email.Create(email);

        private Guid ConverteStringToGuid(string id)
        {
                throw new InvalidOperationException("Id inválido.");
            if (!Guid.TryParse(id, out var guid))

            return guid;
        }

        private Employee EnsureEmployeeExists(Employee? employee)
        {
            if (employee is null)
                throw new InvalidOperationException("Funcionário não econtrado.");

            return employee;
        }


        //CADASTRO DE FUNCIONARIO
        public async Task<Result<Guid>> RegisterEmployee(RegisterEmployeeRequestDTO request)
        {
            var emailVO = CreateVO(request.Email);

            var emailExists = await _employeeRepository.EmailAlreadyRegistered(emailVO);

            if (emailExists)
                throw new InvalidOperationException("Email já cadastrado.");

            var passwordHash = _passwordService.GenerateHash(request.Password);

            var employee = new Employee(request.Name, emailVO, passwordHash, request.RoleAccess);

            await _employeeRepository.CreateAsync(employee);

            return Result<Guid>.Success(employee.Id, "Funcionário cadastrado com sucesso.");

        }

        //LOGIN DO FUNCIONARIO
        public async Task<Result<LoginResponseDTO>> Login(LoginEmployeeRequestDTO request)
        {

            var emailVO = CreateVO(request.Email);

            var employee = await _employeeRepository.GetByEmailAsync(emailVO);


            EnsureEmployeeExists(employee);

            if (!_passwordService.VerifyPassword(request.Password, employee.PasswordHash))
                throw new InvalidOperationException("Login inválido.");


            var employeeToken = new TokenEmployee
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email.ToString(),
                Role = employee.RoleAccess.ToString()
            };

            var token = _tokenService.GenerateToken(employeeToken);

            var loginResponseDTO = new LoginResponseDTO
            (
               employee.Id.ToString(),
               employee.Name,
               employee.RoleAccess.ToString(),
               token

            );

            return Result<LoginResponseDTO>.Success(loginResponseDTO, "Login Realizado com sucesso.");

        }

        //ATUALIZAR SENHA DE LOGIN DO FUNCIONARIO
        public async Task<Result<Guid>> UpdateEmployeePassword(UpdatePasswordEmployeeRequestDTO request)
        {
            var email = CreateVO(request.Email);
            var employee = await _employeeRepository.GetByEmailAsync(email);

            employee = EnsureEmployeeExists(employee);

            var newPassword = _passwordService.GenerateHash(request.Password);
            employee.UpdatePassword(newPassword);

            await _employeeRepository.SaveChangesAsync();

            return Result<Guid>.Success(employee.Id, "Senha atualizada com sucesso");

        }

        //ATUALIZAR EMAIL DO FUNCIONARIO
        public async Task<Result<Guid>> UpdateEmail (UpdateEmailEmployeeDTO request)
        {
            var id = ConverteStringToGuid(request.Id);
            var employee = await _employeeRepository.GetByIdAsync(id);

            employee = EnsureEmployeeExists(employee);

            employee.UpdateEmail(request.Email);

            await _employeeRepository.SaveChangesAsync();

            return Result<Guid>.Success(id, "Email atualizado com sucesso.");

        }

        //ATUALIZAR ROLE DE ACESSO DO FUNCIONARIO
        public async Task<Result<Guid>> UpdateEmployeeChangeRole(UpdateRoleEmployeeDTO request)
        {
            var id = ConverteStringToGuid(request.Id);

            var employee = await _employeeRepository.GetByIdAsync(id);

            employee = EnsureEmployeeExists(employee);

            employee.ChangeRoleAccess(request.Role);
            await _employeeRepository.SaveChangesAsync();

            return Result<Guid>.Success(id, "Role de acesso atualizado com sucesso.");
        }

        //DELETAR FUNCIONARIO
        public async Task<Result<Guid>> DeleteEmployee(DeleteEmployeeRequestDTO request)
        {
            var id = ConverteStringToGuid(request.Id);

            var employee = await _employeeRepository.GetByIdAsync(id);

            employee = EnsureEmployeeExists(employee);

            await _employeeRepository.DeleteAsync(employee);

            return Result<Guid>.Success(id, "Funcionário deletado com sucesso.");
        }


        //LISTAR FUNCIONARIOS
        public async Task<Result<List<ListEmployeeDTO>>> ListEmployee ()
        {
            var employers = await _employeeRepository.GetAllAsync() 
                ?? throw new InvalidOperationException("Nenhum funcionário encontrado.");
            
            var listEmployeeDTO = employers.Select(x => new ListEmployeeDTO 
            (
               x.Id.ToString(),
               x.Name,
               x.RoleAccess.ToString()  
            )).ToList();

            return Result<List<ListEmployeeDTO>>.Success(listEmployeeDTO, "Funcionários listados com sucesso.");
        }


        public async Task<Result<GetByIdEmployeeDTO>> GetEmployeeById (SearchEmployeeIdDTO request)
        {
            var id = ConverteStringToGuid(request.Id);

            var employee = await _employeeRepository.GetByIdAsync(id);

            employee = EnsureEmployeeExists(employee);

            var employeeDto = new GetByIdEmployeeDTO
                (
                    employee.Id.ToString(),
                    employee.Name,
                    employee.Email.ToString(),
                    employee.RoleAccess.ToString()
                );

            return Result<GetByIdEmployeeDTO>.Success(employeeDto, "Funcionário buscado com sucesso");
        }
    }
}