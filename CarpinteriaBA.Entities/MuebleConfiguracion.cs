using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities
{
    // Recargo opcional de un mueble: une un Mueble con una Configuracion extra (ej: IVA, Servicio personalizado).
    // Se aplica como porcentaje sobre el precio acumulado (despues de mano de obra y uso de maquinas).
    public class MuebleConfiguracion : IEntidad
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Mueble))]
        public int IdMueble { get; set; }
        public virtual Mueble Mueble { get; set; } //= null!;

        [ForeignKey(nameof(Configuracion))]
        public int IdConfiguracion { get; set; }
        public virtual Configuracion Configuracion { get; set; } //= null!;
    }
}