using Application.DTOs.TableDTO;
using Application.Service;
using Application.Service.Token;
using Application.UseCase.EmployeeUseCase.EmployeeDTO.Request;
using Application.UseCase.EmployeeUseCase.EmployeeDTO.Response;
using Domain.Employee;
using Domain.Employee.ValueObject;
using Domain.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.EmployeeUseCase
{
    public class EmployeeUseCase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly PasswordService _passwordService;
        private readonly TokenService _tokenService;


        private Email CreateVO (string email)
        {
            return Email.Create(email);
        }

        private Guid ConverteStringToGuid (string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new InvalidOperationException("Id inválido.");

            return guid;
        }

        public async Task<Result<Guid>> RegisterEmployee (RegisterEmployeeRequestDTO request)
        {
            var emailVO = CreateVO(request.Email);

            var emailExit = await _employeeRepository.EmailAlreadyRegistered(emailVO);

            if (emailExit)
                throw new InvalidOperationException("Email já cadastrado.");

            var passwordHash = _passwordService.GenerateHash(request.Password);

            var employee = new Employee(request.Name, emailVO, passwordHash, request.RoleAccess);

            await _employeeRepository.CreateAsync(employee);

            return Result<Guid>.Success(employee.Id, "Funcionário cadastrado com sucesso.");

        }

        public async Task<Result<LoginResponseDTO>> Login (LoginEmployeeRequestDTO request)
        {

            var emailVO = CreateVO(request.Email);

            var employee = await _employeeRepository.GetByEmailAsync(emailVO);

            if (!Guid.TryParse(request.Id, out var idGuid))
                throw new InvalidOperationException("Falha na conversão do ID");

            var employee = await _employeeRepository.GetByIdAsync(idGuid);

            if (employee is null)
                throw new InvalidOperationException("Funcionário não encontrado.");

            if (employee.Email != emailVO)
                throw new InvalidOperationException("Email não confere");


            if (!_passwordService.VerifyPassword(request.Password, employee.PasswordHash))
                throw new InvalidOperationException("Senha não confere.");



            var employeeToken = new TokenEmployee
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email.ToString(),
                Role = employee.RoleAccess.ToString()
            };

            var token = _tokenService.GenerateToken(employeeToken);



            var loginResponseDTO =  new LoginResponseDTO
            {
                Id = employee.Id.ToString(),
                Name = employee.Name,
                Role = employee.RoleAccess.ToString(),
                Token = token

            };

            return Result<LoginResponseDTO>.Success(loginResponseDTO, "Login Realizado com sucesso.");

        }

        public async Task<Result<Guid>> UpdateEmployeePassword(UpdatePasswordEmployeeRequestDTO request)
        {
            var email = CreateVO(request.Email);
            var employee = await _employeeRepository.GetByEmailAsync(email);

            if (employee is null)
                throw new InvalidOperationException("Funcionário não encontrado.");

            var newPassword = _passwordService.GenerateHash(request.Password);
            employee.UpdatePassword(newPassword);

            await _employeeRepository.SaveChangesAsync();

            return Result<Guid>.Success(employee.Id, "Senha atualizada com sucesso");

        }


        public async Task<Result<Guid>> UpdateEmployeeChangeRole (UpdateRoleEmployeeDTO request)
        {
            var id = ConverteStringToGuid(request.Id);

            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
                throw new InvalidOperationException("Funcionário não encontrado.");

            employee.ChangeRoleAccess(request.Role);
            await _employeeRepository.SaveChangesAsync();

            return Result<Guid>.Success(employee.Id, "Role de acesso atualizado com sucesso.");
        }

        public async Task<Result<Guid>> DeleteEmployee ()
        {

        }
    }
}


