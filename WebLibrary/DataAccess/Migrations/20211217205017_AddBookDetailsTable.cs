using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddBookDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetail_BookDetailId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookDetail",
                table: "BookDetail");

            migrationBuilder.RenameTable(
                name: "BookDetail",
                newName: "BookDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookDetails",
                table: "BookDetails",
                column: "BookDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetails_BookDetailId",
                table: "Books",
                column: "BookDetailId",
                principalTable: "BookDetails",
                principalColumn: "BookDetailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetails_BookDetailId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookDetails",
                table: "BookDetails");

            migrationBuilder.RenameTable(
                name: "BookDetails",
                newName: "BookDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookDetail",
                table: "BookDetail",
                column: "BookDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetail_BookDetailId",
                table: "Books",
                column: "BookDetailId",
                principalTable: "BookDetail",
                principalColumn: "BookDetailId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
