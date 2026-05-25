using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities.PRODUCTO
{
    public class Insumo
    {
        public Insumo()
        {
            RecetasMuebles = new HashSet<RecetaMueble>();
        }
        public int IdInsumo { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }
        public decimal StockActual { get; set; }
        public string UnidadMedida { get; set; }
        public decimal PrecioCostoActual { get; set; }

        [ForeignKey(nameof(Proveedor))]
        public int IdProveedor { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<RecetaMueble> RecetasMuebles { get; set; }
    }
}
