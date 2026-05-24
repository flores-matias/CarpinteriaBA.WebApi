using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Entities.VENTA
{
    public class DetallePedido
    {
        public int IdDetallePedido { get; set; }
        //public int PedidoId { get; set; }
        //public int MuebleId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVentaReal { get; set; }
    }
}
