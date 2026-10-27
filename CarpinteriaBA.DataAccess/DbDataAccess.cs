using CarpinteriaBA.Entities;
using CarpinteriaBA.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarpinteriaBA.DataAccess
{
    public class DbDataAccess : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public virtual DbSet<Insumo> Insumos { get; set; }
        public virtual DbSet<Mueble> Muebles { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<RecetaMueble> RecetasMuebles { get; set; }
        public virtual DbSet<DetallePedido> DetallesPedidos { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }

        public virtual DbSet<TipoPago> TiposPagos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Configuracion> Configuraciones { get; set; }

        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();//aca estoy configurando el log para que me muestre los errores detallados en la consola,
                                                                                                                                                        //esto es para poder debuggear mejor y ver que esta pasando con las consultas a la base de datos.
                                                                                                                                                        //Cuando hablo de log me refiero a que me muestre en la consola las consultas SQL que se estan
                                                                                                                                                        //ejecutando, esto es muy util para poder debuggear y ver que esta pasando con las consultas a la
                                                                                                                                                        //base de datos, ademas de que me muestre los errores detallados para poder entender mejor que
                                                                                                                                                        //esta pasando cuando algo sale mal.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Ninguna relacion de las tablas del negocio borra en cascada:
            // si un registro esta en uso (ej: un Proveedor con Insumos), la base rechaza el borrado.
            // Se aplica solo a las entidades de CarpinteriaBA.Entities; las de Identity
            // (CarpinteriaBA.Entities.MicrosoftIdentity) quedan como estan.
            var relacionesDelNegocio = modelBuilder.Model.GetEntityTypes()
                .Where(entidad => entidad.ClrType.Namespace == "CarpinteriaBA.Entities")
                .SelectMany(entidad => entidad.GetForeignKeys());//esto es mejor hacerlo asi que en la migracion con un NoAction porque asi no se rompe la integridad referencial de la base de datos, y ademas es mas facil de mantener, ya que si agrego una nueva entidad con relaciones, no tengo que acordarme de poner el NoAction en la migracion, sino que automaticamente se aplica a todas las relaciones de las entidades del negocio.

            foreach (var relacion in relacionesDelNegocio)
            {
                relacion.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}