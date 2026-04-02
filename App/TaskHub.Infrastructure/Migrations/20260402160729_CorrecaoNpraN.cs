using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoNpraN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_responsavel",
                table: "responsavel");

            migrationBuilder.DropIndex(
                name: "IX_responsavel_idUsuario",
                table: "responsavel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_membros_projeto",
                table: "membros_projeto");

            migrationBuilder.DropIndex(
                name: "IX_membros_projeto_idProjeto",
                table: "membros_projeto");

            migrationBuilder.AlterColumn<string>(
                name: "idUsuario",
                table: "responsavel",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_responsavel",
                table: "responsavel",
                columns: new[] { "idUsuario", "idTarefa" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_membros_projeto",
                table: "membros_projeto",
                columns: new[] { "idProjeto", "idUsuario" });

            migrationBuilder.CreateIndex(
                name: "IX_responsavel_idTarefa",
                table: "responsavel",
                column: "idTarefa");

            migrationBuilder.CreateIndex(
                name: "IX_membros_projeto_idUsuario",
                table: "membros_projeto",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_responsavel",
                table: "responsavel");

            migrationBuilder.DropIndex(
                name: "IX_responsavel_idTarefa",
                table: "responsavel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_membros_projeto",
                table: "membros_projeto");

            migrationBuilder.DropIndex(
                name: "IX_membros_projeto_idUsuario",
                table: "membros_projeto");

            migrationBuilder.AlterColumn<string>(
                name: "idUsuario",
                table: "responsavel",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_responsavel",
                table: "responsavel",
                column: "idTarefa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_membros_projeto",
                table: "membros_projeto",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_responsavel_idUsuario",
                table: "responsavel",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_membros_projeto_idProjeto",
                table: "membros_projeto",
                column: "idProjeto");
        }
    }
}
