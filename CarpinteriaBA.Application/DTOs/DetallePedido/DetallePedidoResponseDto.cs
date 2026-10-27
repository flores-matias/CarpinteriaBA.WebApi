using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.DetallePedido
{
    public class DetallePedidoResponseDto
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }

        public int IdMueble { get; set; }
        // Nombre del mueble, para mostrar en la tabla
        public string Mueble { get; set; } = null!;

        public int Cantidad { get; set; }
        public decimal PrecioVentaReal { get; set; }
        public decimal Subtotal { get; set; }
    }
}