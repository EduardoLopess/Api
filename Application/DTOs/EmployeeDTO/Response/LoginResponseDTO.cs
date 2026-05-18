using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.EmployeeDTO.Response
{
    public class LoginResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

    }
}
