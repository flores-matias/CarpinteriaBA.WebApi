using CarpinteriaBA.Abstactions;
using CarpinteriaBA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Pedido:IEntidad
    {
        public Pedido()
        {
            DetallesPedidos = new HashSet<DetallePedido>();
            Pagos = new HashSet<Pago>();
        }
        public int Id { get; set; }
        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get;  set; }
        //public virtual string NombreCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEntregaPrometida { get; set; }
        public decimal TotalAPagar { get; set; }
        public string EstadoPedido { get; set; } = null!;
        public string EstadoPago { get; set; } = null!;
        public virtual ICollection<DetallePedido> DetallesPedidos { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
