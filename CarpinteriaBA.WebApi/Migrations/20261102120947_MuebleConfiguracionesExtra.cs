using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpinteriaBA.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class MuebleConfiguracionesExtra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MueblesConfiguraciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMueble = table.Column<int>(type: "int", nullable: false),
                    IdConfiguracion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MueblesConfiguraciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MueblesConfiguraciones_Configuraciones_IdConfiguracion",
                        column: x => x.IdConfiguracion,
                        principalTable: "Configuraciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MueblesConfiguraciones_Muebles_IdMueble",
                        column: x => x.IdMueble,
                        principalTable: "Muebles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MueblesConfiguraciones_IdConfiguracion",
                table: "MueblesConfiguraciones",
                column: "IdConfiguracion");

            migrationBuilder.CreateIndex(
                name: "IX_MueblesConfiguraciones_IdMueble",
                table: "MueblesConfiguraciones",
                column: "IdMueble");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MueblesConfiguraciones");
        }
    }
}
