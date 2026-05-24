using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Entities.VENTA
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        //public int IdCliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEntregaPrometida { get; set; }
        public decimal TotalAPagar { get; set; }
        public string EstadoPedido { get; set; } = null!;
        public string EstadoPago { get; set; } = null!;
    }
}
