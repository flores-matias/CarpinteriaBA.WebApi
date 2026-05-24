using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Entities.PRODUCTO
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
