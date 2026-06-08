using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.Insumo
{
    public class InsumoResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal StockActual { get; set; }
        public string UnidadMedida { get; set; } = null!;
        public decimal PrecioCostoActual { get; set; }
        public int IdProveedor { get; set; }
    }
}
