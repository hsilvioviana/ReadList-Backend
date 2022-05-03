using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadList.Infraestructure.Migrations
{
    public partial class books_genres_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books_genres_relations",
                schema: "readlist",
                columns: table => new
                {
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books_genres_relations", x => new { x.book_id, x.genre_id });
                    table.ForeignKey(
                        name: "FK_books_genres_relations_books_book_id",
                        column: x => x.book_id,
                        principalSchema: "readlist",
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_books_genres_relations_genres_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "readlist",
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_genres_relations_genre_id",
                schema: "readlist",
                table: "books_genres_relations",
                column: "genre_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books_genres_relations",
                schema: "readlist");
        }
    }
}
