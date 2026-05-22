using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service.password
{
    public interface IPasswordService
    {
        string GenerateHash(string password);
        bool VerifyPassword(string password, string hash);
    }

}
