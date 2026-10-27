using System.Linq;

namespace CarpinteriaBA.Enums
{
    /// <summary>
    /// Estados posibles de un pedido. Se guardan como texto en la columna Pedidos.EstadoPedido.
    /// </summary>
    public static class EstadosPedido
    {
        public const string Pendiente = "Pendiente";
        public const string EnProduccion = "En producción";
        public const string Terminado = "Terminado";
        public const string Entregado = "Entregado";
        public const string Cancelado = "Cancelado";

        // En este orden se muestran en el front
        public static readonly string[] Todos =
        {
            Pendiente, EnProduccion, Terminado, Entregado, Cancelado
        };

        public static bool EsValido(string estado) => Todos.Contains(estado);
    }
}