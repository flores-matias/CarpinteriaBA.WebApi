using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Insumo:IEntidad
    {
        public Insumo()
        {
            RecetasMuebles = new HashSet<RecetaMueble>();
        }
        public int Id { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }
        public decimal StockActual { get; set; }
        public string UnidadMedida { get; set; }
        public decimal PrecioCostoActual { get; private set; }
        public void ActualizarPrecioCosto(decimal nuevoPrecio)
        {
            if (nuevoPrecio <= 0)
            {
                throw new ArgumentException("El precio de costo debe ser mayor que cero.");
            }
            PrecioCostoActual = nuevoPrecio;
        }

        [ForeignKey(nameof(Proveedor))]
        public int IdProveedor { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<RecetaMueble> RecetasMuebles { get; set; }
        
    }
}
