using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddStoreProcedureAndView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW dbo.GetOnlyBookDetails
                                   AS 
                                   SELECT b.ISBN, b.Title, b.Price FROM dbo.Books b");
            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.getAllBookDetails
                                   @bookId int
                                   AS
                                    SET NOCOUNT ON;
                                    SELECT * FROM dbo.Books b
                                    WHERE b.BookId = @bookId;
                                    SET NOCOUNT OFF;
                                   GO");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.GetOnlyBookDetails");
            migrationBuilder.Sql("DROP PROCEDURE dbo.getAllBookDetails");
        }
    }
}
