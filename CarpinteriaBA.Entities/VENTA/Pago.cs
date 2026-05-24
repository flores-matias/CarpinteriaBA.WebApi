using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Entities.VENTA
{
    public class Pago
    {
        public int IdPago { get; set; }
        //public int IdTipoDePago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
