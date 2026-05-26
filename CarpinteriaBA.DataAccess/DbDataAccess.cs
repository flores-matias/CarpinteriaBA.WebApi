using CarpinteriaBA.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.DataAccess
{
    public class DbDataAccess:IdentityDbContext
    {
        public virtual DbSet<Insumo> Insumos { get; set; }
        public virtual DbSet<Mueble> Muebles { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<RecetaMueble> RecetasMuebles { get; set; }
        public virtual DbSet<DetallePedido> DetallesPedidos { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }

        public virtual DbSet<TipoPago>TiposPagos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Configuracion> Configuraciones { get; set; }

        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();//aca estoy configurando el log para que me muestre los errores detallados en la consola,
                                                                                                                                                        //esto es para poder debuggear mejor y ver que esta pasando con las consultas a la base de datos.
                                                                                                                                                        //Cuando hablo de log me refiero a que me muestre en la consola las consultas SQL que se estan
                                                                                                                                                        //ejecutando, esto es muy util para poder debuggear y ver que esta pasando con las consultas a la
                                                                                                                                                        //base de datos, ademas de que me muestre los errores detallados para poder entender mejor que
                                                                                                                                                        //esta pasando cuando algo sale mal.
    }
}
