﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddedOneToOneBookAndBookDetailRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookDetailId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookDetail",
                columns: table => new
                {
                    BookDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfChapters = table.Column<int>(type: "int", nullable: false),
                    NumberOfPages = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetail", x => x.BookDetailId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookDetailId",
                table: "Books",
                column: "BookDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetail_BookDetailId",
                table: "Books",
                column: "BookDetailId",
                principalTable: "BookDetail",
                principalColumn: "BookDetailId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetail_BookDetailId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookDetail");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookDetailId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookDetailId",
                table: "Books");
        }
    }
}
