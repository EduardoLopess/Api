using Application.DTOs.EmployeeDTO.Request;
using Application.DTOs.EmployeeDTO.Response;
using Application.DTOs.TableDTO;
using Application.Service;
using Application.Service.Token;
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
    }
}


