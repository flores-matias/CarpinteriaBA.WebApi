using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Mueble:IEntidad
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
    }
}
