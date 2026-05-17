using Domain.Employee.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.EmployeeDTO.Request
{
    public class RegisterEmployeeRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public RoleAccess RoleAccess { get; set; } 
    }
}
