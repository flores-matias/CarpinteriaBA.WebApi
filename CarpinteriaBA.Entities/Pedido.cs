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
        //Suma de todos los pagos del pedido (ej: seña + saldo)
        [NotMapped]
        public decimal TotalPagado => Pagos.Sum(p => p.Monto);

        //Lo que falta pagar
        [NotMapped]
        public decimal Saldo => TotalAPagar - TotalPagado;

        // ---- Reglas del negocio ----
        /// Recalcula el total como la suma de los detalles (cantidad x precio de venta)
        /// y actualiza el estado de pago. Se llama cada vez que se agrega, edita o borra un detalle.
        public void RecalcularTotalAPagar()
        {
            TotalAPagar = DetallesPedidos.Sum(d => d.Cantidad * d.PrecioVentaReal);
            ActualizarEstadoPago();
        }

        
        /// Pendiente: no se pago nada. Parcial: se pago una parte. Pagado: se cubrio el total.
        /// Se llama cada vez que se agrega, edita o borra un pago.
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