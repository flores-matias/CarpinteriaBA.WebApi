using CarpinteriaBA.Abstactions;
using CarpinteriaBA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Pago : IEntidad
    {
        public int Id { get; set; }

        
        [ForeignKey(nameof(Pedido))]
        public int IdPedido { get; set; }
        public virtual Pedido Pedido { get; set; } //= null!;

        [ForeignKey(nameof(TipoPago))]
        public int IdTipoPago { get; set; }
        public virtual TipoPago TipoPago { get; set; } //= null!;
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}