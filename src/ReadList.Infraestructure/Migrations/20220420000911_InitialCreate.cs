using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadList.Infraestructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "readlist");

            migrationBuilder.CreateTable(
                name: "teste",
                schema: "readlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: true),
                    numero = table.Column<int>(type: "integer", nullable: true),
                    datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    datamodificacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teste", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teste",
                schema: "readlist");
        }
    }
}
