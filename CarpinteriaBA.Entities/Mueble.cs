using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Mueble : IEntidad
    {
        public Mueble()
        {
            RecetasMuebles = new HashSet<RecetaMueble>();
            DetallesPedidos = new HashSet<DetallePedido>();
        }
        public int Id { get; set; }
        [StringLength(30)]
        public string Nombre { get; set; } //= null!;
        [StringLength(200)]
        public string Descripcion { get; set; } //= null!;
        public decimal PrecioSugerido { get; set; }
        public virtual ICollection<RecetaMueble> RecetasMuebles { get; set; }
        public virtual ICollection<DetallePedido> DetallesPedidos { get; set; }

        /// <summary>
        /// Suma del costo de todos los insumos de la receta, con precios de costo ACTUALES.
        /// Calculado: no se guarda en la base.
        /// </summary>
        [NotMapped]
        public decimal CostoMateriales => RecetasMuebles.Sum(r => r.Subtotal);
    }
}