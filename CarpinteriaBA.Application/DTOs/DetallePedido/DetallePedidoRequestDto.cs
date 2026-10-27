using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor que cero.")]
        public decimal PrecioVentaReal { get; set; }
    }
}