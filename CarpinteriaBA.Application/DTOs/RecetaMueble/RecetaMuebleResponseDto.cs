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
        // Datos del insumo, para mostrar en la tabla sin buscarlos aparte
        public string Insumo { get; set; } = null!;
        public string UnidadMedida { get; set; } = null!;
        public decimal PrecioCostoActual { get; set; }

        public decimal CantidadConDesperdicio { get; set; }
        public decimal Subtotal { get; set; }
    }
}