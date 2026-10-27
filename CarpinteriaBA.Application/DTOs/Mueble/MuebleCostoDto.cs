namespace CarpinteriaBA.Application.DTOs.Mueble
{
    // <summary>
    // Desglose del precio de un mueble:
    // Materiales + Mano de obra (materiales x %) + Uso de maquinas (materiales x %) = Precio calculado.
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

        public decimal PrecioCalculado { get; set; }
        public decimal PrecioSugerido { get; set; }

        // Positivo: el sugerido esta por encima del calculado. Negativo: por debajo.
        public decimal Diferencia { get; set; }

        public bool TieneReceta { get; set; }
        public bool MargenesConfigurados { get; set; }
    }
}