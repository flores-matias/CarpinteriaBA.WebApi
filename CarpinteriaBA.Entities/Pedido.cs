using CarpinteriaBA.Abstactions;
using CarpinteriaBA.Entities;
using CarpinteriaBA.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Pedido : IEntidad
    {
        public Pedido()
        {
            DetallesPedidos = new HashSet<DetallePedido>();
            Pagos = new HashSet<Pago>();
        }
        public int Id { get; set; }
        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get; set; }
        //public virtual string NombreCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEntregaPrometida { get; set; }
        public decimal TotalAPagar { get; set; }
        public string EstadoPedido { get; set; } = null!;
        public string EstadoPago { get; set; } = null!;
        public virtual ICollection<DetallePedido> DetallesPedidos { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }

        // ---- Calculados: no se guardan en la base ----

        //Suma de todos los pagos del pedido (ej: seña + saldo).
        [NotMapped]
        public decimal TotalPagado => Pagos.Sum(p => p.Monto);

        //Lo que falta pagar.
        [NotMapped]
        public decimal Saldo => TotalAPagar - TotalPagado;

        //Suma de los subtotales de los detalles cargados actualmente.
        [NotMapped]
        public decimal TotalDetalles => DetallesPedidos.Sum(d => d.Subtotal);

        //Entregado o Cancelado: ya no se le pueden cambiar los muebles.
        [NotMapped]
        public bool EstaCerrado =>
            EstadoPedido == EstadosPedido.Entregado || EstadoPedido == EstadosPedido.Cancelado;

        // ---- Reglas del negocio ----

        //Fija el nuevo total del pedido (suma de sus detalles) y actualiza el estado de pago.
        //El total no puede quedar por debajo de lo que ya se pago.
        //Se llama cada vez que se agrega, edita o quita un detalle.
        public void ActualizarTotalAPagar(decimal nuevoTotal)
        {
            if (nuevoTotal < TotalPagado)
            {
                throw new ArgumentException(
                    $"El total del pedido (${nuevoTotal:N2}) no puede quedar por debajo de lo ya pagado (${TotalPagado:N2}).");
            }
            TotalAPagar = nuevoTotal;
            ActualizarEstadoPago();
        }

        /// <summary>
        /// Pendiente: no se pago nada. Parcial: se pago una parte. Pagado: se cubrio el total.
        /// Se llama cada vez que se agrega, edita o borra un pago.
        /// </summary>
        public void ActualizarEstadoPago()
        {
            decimal pagado = TotalPagado;

            if (pagado <= 0)
            {
                EstadoPago = EstadosPago.Pendiente;
            }
            else if (pagado < TotalAPagar)
            {
                EstadoPago = EstadosPago.Parcial;
            }
            else
            {
                EstadoPago = EstadosPago.Pagado;
            }
        }
    }
}