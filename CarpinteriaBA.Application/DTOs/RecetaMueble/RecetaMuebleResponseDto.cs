using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.RecetaMueble
{
    public class RecetaMuebleResponseDto
    {
        public int Id { get; set; }
        public int IdMueble { get; set; }
        public int IdInsumo { get; set; }
        public decimal CantidadConDesperdicio { get; set; }
    }
}
