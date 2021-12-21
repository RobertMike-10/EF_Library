using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddComplexProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.getAllBookDetailsFull
                                   (@bookId int =0,
                                    @title VARCHAR(100) = NULL
                                   )
                                   AS
                                    SET NOCOUNT ON;
                                    SELECT B.Title, B.ISBN, B.Price, BD.NumberOfPages,
                                           P.Name Publisher, C.Name Category FROM dbo.Books B
                                    LEFT JOIN BookDetails BD ON BD.BookDetailId = B.BookDetailId
                                    LEFT JOIN Publishers P ON P.PublisherId = B.PublisherId
                                    LEFT JOIN Categories C ON C.CategoryId = B.CategoryId
                                    WHERE B.BookId = @bookId OR B.Title like '%@title%';
                                    SET NOCOUNT OFF;
                                   GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.getAllBookDetailsFull");
        }
    }
}
