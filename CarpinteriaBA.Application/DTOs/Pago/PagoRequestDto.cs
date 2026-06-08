using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpinteriaBA.Application.DTOs.Pago
{
    public class PagoRequestDto
    {
        [ForeignKey(nameof(TipoDePago))]
        public int IdTipoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
