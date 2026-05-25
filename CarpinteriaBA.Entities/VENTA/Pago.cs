using CarpinteriaBA.Entities.PRODUCTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities.VENTA
{
    public class Pago
    {
        public int IdPago { get; set; }
        [ForeignKey(nameof(TipoPago))]
        public int IdTipoPago { get; set; }
        public virtual TipoPago TipoPago { get; set; } //= null!;
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
