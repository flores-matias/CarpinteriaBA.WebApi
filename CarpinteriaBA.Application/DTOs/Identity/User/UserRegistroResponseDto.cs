using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.Identity.User
{
    public class UserRegistroResponseDto
    {
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
