using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjPacientes.Migrations
{
    /// <inheritdoc />
    public partial class CrearRelacionPacienteReceta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecetaId",
                table: "Pacientes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_RecetaId",
                table: "Pacientes",
                column: "RecetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Recetas_RecetaId",
                table: "Pacientes",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Recetas_RecetaId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_RecetaId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "RecetaId",
                table: "Pacientes");
        }
    }
}
