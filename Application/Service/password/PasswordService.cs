using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service.password
{
    public class PasswordService : IPasswordService
    {
    
        public string GenerateHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword (string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
