using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.Mueble
{
    public class MuebleResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal PrecioSugerido { get; set; }

        // Calculados con la receta y los margenes de Configuracion (no se guardan)
        public decimal CostoMateriales { get; set; }
        public decimal PrecioCalculado { get; set; }
    }
}