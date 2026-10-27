using System.Linq;

namespace CarpinteriaBA.Enums
{
   
    // Claves de la tabla Configuraciones que usa el sistema para calcular el precio de los muebles.
    // Tienen que coincidir EXACTO con la columna Clave de la base.
    // No se pueden renombrar ni borrar desde la API.
    public static class ClavesConfiguracion
    {
        // Proporcion sobre el costo de materiales: 1,5 = 150%
        public const string ManoDeObra = "Mano de obra";

        // Proporcion sobre el costo de materiales: 0,1 = 10%
        public const string UsoDeMaquinas = "Uso de maquinas";

        public static readonly string[] Protegidas = { ManoDeObra, UsoDeMaquinas };

        public static bool EsProtegida(string clave) => Protegidas.Contains(clave);
    }
}