using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class TipoPago:IEntidad
    {
        public TipoPago()
        {
            Pagos = new HashSet<Pago>();
        }
        public int Id { get; set; }
        [StringLength(200)]
        public string Descripcion { get; set; } = null!;
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
