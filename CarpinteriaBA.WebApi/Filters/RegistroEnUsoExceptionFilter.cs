using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CarpinteriaBA.WebApi.Filters
{
    /// <summary>
    /// Convierte los errores de clave foranea de SQL Server (error 547) en un 400 con mensaje claro.
    /// Ej: borrar un Proveedor que tiene Insumos -> "No se puede eliminar porque hay insumos que lo usan."
    /// Se registra una sola vez en Program.cs y aplica a todos los controllers.
    /// </summary>
    public class RegistroEnUsoExceptionFilter : IExceptionFilter
    {
        // Numero de error de SQL Server para "conflicto con una restriccion de clave foranea"
        private const int ErrorSqlClaveForanea = 547;

        // Nombre de cada tabla (como esta en la base) -> como se muestra en el mensaje
        private static readonly Dictionary<string, string> NombresTablas = new()
        {
            ["Insumos"] = "insumos",
            ["Pagos"] = "pagos",
            ["Pedidos"] = "pedidos",
            ["DetallesPedidos"] = "detalles de pedidos",
            ["RecetasMuebles"] = "recetas de muebles"
        };

        public void OnException(ExceptionContext context)
        {
            // Solo se ocupa del error 547; cualquier otro error sigue su camino normal
            if (context.Exception is not DbUpdateException dbEx
                || dbEx.InnerException is not SqlException sqlEx
                || sqlEx.Number != ErrorSqlClaveForanea)
            {
                return;
            }

            // El mensaje trae el nombre de la restriccion, ej: FK_Insumos_Proveedors_IdProveedor.
            // La primera tabla (Insumos) es la que usa el registro.
            var coincidencia = Regex.Match(sqlEx.Message, @"FK_([A-Za-z]+)_");
            var tabla = coincidencia.Success
                && NombresTablas.TryGetValue(coincidencia.Groups[1].Value, out var nombre)
                    ? nombre
                    : "otros registros";

            // Si se estaba borrando, el registro esta en uso.
            // Si se estaba creando o editando, se eligio un dato relacionado que no existe.
            bool esBorrado = dbEx.Entries.Any(e => e.State == EntityState.Deleted);

            string mensaje = esBorrado
                ? $"No se puede eliminar porque hay {tabla} que lo usan."
                : "Uno de los datos seleccionados no existe. Revisa los valores elegidos.";

            context.Result = new BadRequestObjectResult(mensaje);
            context.ExceptionHandled = true;
        }
    }
}