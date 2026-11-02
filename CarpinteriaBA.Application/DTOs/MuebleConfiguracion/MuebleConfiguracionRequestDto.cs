using System.ComponentModel.DataAnnotations.Schema;

namespace CarpinteriaBA.Application.DTOs.MuebleConfiguracion
{
    public class MuebleConfiguracionRequestDto
    {
        [ForeignKey(nameof(Mueble))]
        public int IdMueble { get; set; }

        [ForeignKey(nameof(Configuracion))]
        public int IdConfiguracion { get; set; }
    }
}