using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class RecetaMueble : IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Mueble))]
        public int IdMueble { get; set; }
        public virtual Mueble Mueble { get; set; }//= null!;
        [ForeignKey(nameof(Insumo))]
        public int IdInsumo { get; set; }
        public virtual Insumo Insumo { get; set; } //= null!;
        public decimal CantidadConDesperdicio { get; set; }

        // Costo de este insumo en el mueble: cantidad con desperdicio x precio de costo ACTUAL del insumo.
        // Calculado: si sube el insumo, sube solo. No se guarda en la base.
        [NotMapped]
        public decimal Subtotal => CantidadConDesperdicio * (Insumo?.PrecioCostoActual ?? 0);
    }
}