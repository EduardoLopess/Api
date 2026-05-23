using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service.Token
{
    public record TokenEmployee
    (Guid Id,
     string Name,
     string Email,
     string Role);
        
}
