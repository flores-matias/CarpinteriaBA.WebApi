using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpinteriaBA.Application.DTOs.TipoDePago
{
    public class TipoPagoRequestDto
    {
        
        [StringLength(200)]
        public string Descripcion { get; set; } = null!;
    }
}
