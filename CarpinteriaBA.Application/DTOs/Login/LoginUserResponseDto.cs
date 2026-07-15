using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.Login
{
    public class LoginUserResponseDto
    {
        public string Token { get; set; }
        public string? UserName { get; set; }
        public string? Mail { get; set; }
        public bool Login { get; set; }
        public List<string> Errores { get; set; }
    }
}
