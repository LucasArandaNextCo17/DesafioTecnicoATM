using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM.Migrations
{
    public partial class AgregarIntentosFallidosATarjeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Pin = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    Bloqueada = table.Column<bool>(type: "bit", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IntentosFallidos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Operaciones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDTarjeta = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false),
                    CodigoOperacion = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    MontoRetirado = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operaciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Operacion__IDTar__3B75D760",
                        column: x => x.IDTarjeta,
                        principalTable: "Tarjetas",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_IDTarjeta",
                table: "Operaciones",
                column: "IDTarjeta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operaciones");

            migrationBuilder.DropTable(
                name: "Tarjetas");
        }
    }
}
