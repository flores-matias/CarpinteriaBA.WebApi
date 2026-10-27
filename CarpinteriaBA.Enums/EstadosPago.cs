namespace CarpinteriaBA.Enums
{

    /// Estados de pago de un pedido. No los elige el usuario:
    /// se calculan con Pedido.ActualizarEstadoPago() segun lo pagado vs. el total.
    public static class EstadosPago
    {
        public const string Pendiente = "Pendiente";
        public const string Parcial = "Parcial";
        public const string Pagado = "Pagado";
    }
}