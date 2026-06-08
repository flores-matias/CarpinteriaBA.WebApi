using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.DetallePedido
{
    public class DetallePedidoRequestDto
    {
        [ForeignKey(nameof(Pedido))]
        public int IdPedido { get; set; }
        [ForeignKey(nameof(Mueble))]
        public int IdMueble { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVentaReal { get; set; }
    }
}
