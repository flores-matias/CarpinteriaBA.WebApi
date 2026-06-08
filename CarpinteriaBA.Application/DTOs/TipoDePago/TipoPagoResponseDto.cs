using System.ComponentModel.DataAnnotations;

namespace CarpinteriaBA.Application.DTOs.TipoDePago
{
    public class TipoPagoResponseDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
    }
}
