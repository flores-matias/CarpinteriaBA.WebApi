using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.Insumo
{
    public class InsumoRequestDto
    {

        [StringLength(30)]
        public string Nombre { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public decimal StockActual { get; set; }

        public string UnidadMedida { get; set; } = null!;

        // Misma regla que Insumo.ActualizarPrecioCosto: tiene que ser mayor a 0
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de costo debe ser mayor que cero.")]
        public decimal PrecioCostoActual { get; set; }

        [ForeignKey(nameof(Proveedor))]
        public int IdProveedor { get; set; }
    }
}