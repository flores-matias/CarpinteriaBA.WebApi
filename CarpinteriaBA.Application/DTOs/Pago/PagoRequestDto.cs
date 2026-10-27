using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpinteriaBA.Application.DTOs.Pago
{
    public class PagoRequestDto
    {
        [ForeignKey(nameof(Pedido))]
        public int IdPedido { get; set; }

        [ForeignKey(nameof(TipoDePago))]
        public int IdTipoPago { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }

        public DateTime FechaPago { get; set; }
    }
}
