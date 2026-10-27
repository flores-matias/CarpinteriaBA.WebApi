using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class DetallePedido : IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Pedido))]
        public int IdPedido { get; set; }
        public virtual Pedido Pedido { get; set; }//= null!;
        [ForeignKey(nameof(Mueble))]
        public int IdMueble { get; set; }
        public virtual Mueble Mueble { get; set; }//= null!;
        public int Cantidad { get; set; }
        public decimal PrecioVentaReal { get; set; }

        //Cantidad x precio de venta. Calculado: no se guarda en la base.
        [NotMapped]
        public decimal Subtotal => Cantidad * PrecioVentaReal;
    }
}