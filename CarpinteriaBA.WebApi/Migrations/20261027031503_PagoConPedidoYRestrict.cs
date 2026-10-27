using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpinteriaBA.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class PagoConPedidoYRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedidos_Muebles_IdMueble",
                table: "DetallesPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedidos_Pedidos_IdPedido",
                table: "DetallesPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumos_Proveedors_IdProveedor",
                table: "Insumos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Pedidos_PedidoId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_TiposPagos_IdTipoPago",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_IdCliente",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetasMuebles_Insumos_IdInsumo",
                table: "RecetasMuebles");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetasMuebles_Muebles_IdMueble",
                table: "RecetasMuebles");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_PedidoId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Pagos");

            migrationBuilder.AddColumn<int>(
                name: "IdPedido",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdPedido",
                table: "Pagos",
                column: "IdPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedidos_Muebles_IdMueble",
                table: "DetallesPedidos",
                column: "IdMueble",
                principalTable: "Muebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedidos_Pedidos_IdPedido",
                table: "DetallesPedidos",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Insumos_Proveedors_IdProveedor",
                table: "Insumos",
                column: "IdProveedor",
                principalTable: "Proveedors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Pedidos_IdPedido",
                table: "Pagos",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_TiposPagos_IdTipoPago",
                table: "Pagos",
                column: "IdTipoPago",
                principalTable: "TiposPagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_IdCliente",
                table: "Pedidos",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetasMuebles_Insumos_IdInsumo",
                table: "RecetasMuebles",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetasMuebles_Muebles_IdMueble",
                table: "RecetasMuebles",
                column: "IdMueble",
                principalTable: "Muebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedidos_Muebles_IdMueble",
                table: "DetallesPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedidos_Pedidos_IdPedido",
                table: "DetallesPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumos_Proveedors_IdProveedor",
                table: "Insumos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Pedidos_IdPedido",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_TiposPagos_IdTipoPago",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_IdCliente",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetasMuebles_Insumos_IdInsumo",
                table: "RecetasMuebles");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetasMuebles_Muebles_IdMueble",
                table: "RecetasMuebles");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_IdPedido",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "IdPedido",
                table: "Pagos");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Pagos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_PedidoId",
                table: "Pagos",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedidos_Muebles_IdMueble",
                table: "DetallesPedidos",
                column: "IdMueble",
                principalTable: "Muebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedidos_Pedidos_IdPedido",
                table: "DetallesPedidos",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insumos_Proveedors_IdProveedor",
                table: "Insumos",
                column: "IdProveedor",
                principalTable: "Proveedors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Pedidos_PedidoId",
                table: "Pagos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_TiposPagos_IdTipoPago",
                table: "Pagos",
                column: "IdTipoPago",
                principalTable: "TiposPagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_IdCliente",
                table: "Pedidos",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetasMuebles_Insumos_IdInsumo",
                table: "RecetasMuebles",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetasMuebles_Muebles_IdMueble",
                table: "RecetasMuebles",
                column: "IdMueble",
                principalTable: "Muebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
