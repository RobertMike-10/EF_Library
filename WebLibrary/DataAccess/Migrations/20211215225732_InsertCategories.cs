using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InsertCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories VALUES ('Enciclopedia');");
            migrationBuilder.Sql("INSERT INTO Categories VALUES ('Novela');");
            migrationBuilder.Sql("INSERT INTO Categories VALUES ('Didáctico');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
