using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UX_MembroProjeto_Dono : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UX_MembroProjeto_Dono",
                table: "membros_projeto",
                column: "idProjeto",
                unique: true,
                filter: "\"privilegio\" = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_MembroProjeto_Dono",
                table: "membros_projeto");
        }
    }
}
