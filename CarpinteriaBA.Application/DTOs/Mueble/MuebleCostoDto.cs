using System.Collections.Generic;

namespace CarpinteriaBA.Application.DTOs.Mueble
{
    /// <summary>
    /// Desglose del precio de un mueble:
    /// Materiales + Mano de obra + Uso de maquinas = Subtotal
    /// Subtotal + recargos opcionales (cada uno sobre el acumulado) = Precio calculado.
    /// </summary>
    public class MuebleCostoDto
    {
        public int IdMueble { get; set; }
        public string Mueble { get; set; } = null!;

        public decimal CostoMateriales { get; set; }

        // Proporcion configurada (1,5 = 150%) y monto resultante
        public decimal ProporcionManoDeObra { get; set; }
        public decimal ManoDeObra { get; set; }

        public decimal ProporcionUsoDeMaquinas { get; set; }
        public decimal UsoDeMaquinas { get; set; }

        // Materiales + mano de obra + uso de maquinas (antes de los recargos)
        public decimal Subtotal { get; set; }

        // Recargos opcionales del mueble, en el orden en que se aplican
        public List<RecargoCostoDto> Recargos { get; set; } = new();

        public decimal PrecioCalculado { get; set; }
        public decimal PrecioSugerido { get; set; }

        // Positivo: el sugerido esta por encima del calculado. Negativo: por debajo.
        public decimal Diferencia { get; set; }

        public bool TieneReceta { get; set; }
        public bool MargenesConfigurados { get; set; }
    }

    /// <summary>Un renglon de recargo en el desglose (ej: IVA 21% sobre $28.600 = $6.006).</summary>
    public class RecargoCostoDto
    {
        // Id del registro MuebleConfiguracion (para poder quitar el recargo desde el front)
        public int IdMuebleConfiguracion { get; set; }
        public int IdConfiguracion { get; set; }
        public string Clave { get; set; } = null!;

        // Proporcion configurada (0,21 = 21%)
        public decimal Proporcion { get; set; }

        // Sobre que monto se calculo (el precio acumulado hasta ese momento)
        public decimal BaseCalculo { get; set; }
        public decimal Monto { get; set; }
    }
}