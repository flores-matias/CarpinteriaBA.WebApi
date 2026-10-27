using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.Pedido
{
    
    // Datos que carga el usuario. FechaCreacion, TotalAPagar y EstadoPago
    // NO vienen del front: los calcula la API.
    public class PedidoRequestDto
    {
        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get; set; }

        public DateTime FechaEntregaPrometida { get; set; }

        [Required]
        public string EstadoPedido { get; set; } = null!;
    }
}