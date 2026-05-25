using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using System.Text;

namespace CarpinteriaBA.Entities.VENTA
{
    public class TipoPago
    {
        public TipoPago()
        {
            Pagos = new HashSet<Pago>();
        }
        public int IdTipoPago { get; set; }
        [StringLength(200)]
        public string Descripcion { get; set; } = null!;
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
