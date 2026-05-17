using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.EmployeeDTO.Request
{
    public class LoginEmployeeRequestDTO
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
