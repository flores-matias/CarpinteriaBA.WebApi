using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Entities.PRODUCTO
{
    public class Insumo
    {
        public int IdInsumo { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal StockActual { get; set; }
        public string UnidadMedida { get; set; } = null!;
        public decimal PrecioCostoActual { get; set; }
        //public int IdProveedor { get; set; }
    }
}
