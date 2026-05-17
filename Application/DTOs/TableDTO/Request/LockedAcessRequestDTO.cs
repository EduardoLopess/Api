using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.TableDTO.Request
{
    public class LockedAcessRequestDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string TableId { get; set; } = string.Empty;
    }
}
