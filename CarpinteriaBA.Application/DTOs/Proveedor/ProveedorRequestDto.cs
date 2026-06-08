using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.Proveedor
{
    public class ProveedorRequestDto
    {
        public string RazonSocial { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
