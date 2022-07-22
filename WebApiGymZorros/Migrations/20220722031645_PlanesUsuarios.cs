using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiGymZorros.Migrations
{
    public partial class PlanesUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Planes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlanesUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanesUsuarios_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanesUsuarios_Planes_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Planes_ApplicationUserId",
                table: "Planes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesUsuarios_PlanId",
                table: "PlanesUsuarios",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesUsuarios_UsuarioId",
                table: "PlanesUsuarios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planes_AspNetUsers_ApplicationUserId",
                table: "Planes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planes_AspNetUsers_ApplicationUserId",
                table: "Planes");

            migrationBuilder.DropTable(
                name: "PlanesUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_Planes_ApplicationUserId",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Planes");
        }
    }
}
