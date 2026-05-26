using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Configuracion: IEntidad
    {
        public int Id { get; set; }
        public string Clave { get; set; } = null!;
        public decimal Valor { get; set; }
        [StringLength(200)]
        public string Descripcion { get; set; } = null!;
    }
}
