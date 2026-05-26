using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Cliente:IEntidad
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }
        public int Id { get; set; }
        [StringLength(30)]
        public string Nombre { get; set; } = null!;
        [StringLength(30)]
        public string Apellido { get; set; } = null!;
        [StringLength(20)]
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
