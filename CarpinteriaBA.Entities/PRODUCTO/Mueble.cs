using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Entities.PRODUCTO
{
    public class Mueble
    {
        public int IdMueble { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal PrecioSugerido { get; set; }
    }
}
