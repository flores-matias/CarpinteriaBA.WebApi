using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.RecetaMueble
{
    public class RecetaMuebleRequestDto
    {
        [ForeignKey(nameof(Mueble))]
        public int IdMueble { get; set; }

        [ForeignKey(nameof(Insumo))]
        public int IdInsumo { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public decimal CantidadConDesperdicio { get; set; }
    }
}