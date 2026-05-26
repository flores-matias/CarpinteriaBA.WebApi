using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class RecetaMueble:IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Mueble))]
        public int IdMueble { get; set; }
        public virtual Mueble Mueble { get; set; }//= null!;
        [ForeignKey(nameof(Insumo))]
        public int IdInsumo { get; set; }
        public virtual Insumo Insumo { get; set; } //= null!;
        public decimal CantidadConDesperdicio { get; set; }

    }
}
