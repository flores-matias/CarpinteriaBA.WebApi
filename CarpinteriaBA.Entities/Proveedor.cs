using CarpinteriaBA.Abstactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Entities
{
    public class Proveedor:IEntidad
    {
        public Proveedor()
        {
            Insumos = new HashSet<Insumo>();
        }
        public int Id { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public virtual ICollection<Insumo> Insumos { get; set; }
    }
}
