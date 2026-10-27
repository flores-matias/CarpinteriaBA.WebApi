using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.Pago
{
    public class PagoResponseDto
    {
        public int Id { get; set; }

        public int IdPedido { get; set; }
        // "Nombre Apellido" del cliente del pedido, para mostrar en la tabla
        public string Cliente { get; set; } = null!;

        public int IdTipoPago { get; set; }
        // Descripcion del tipo de pago (ej: "Efectivo"), para mostrar en la tabla
        public string TipoPago { get; set; } = null!;

        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}