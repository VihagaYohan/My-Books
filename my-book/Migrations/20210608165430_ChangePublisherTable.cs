using Microsoft.EntityFrameworkCore.Migrations;

namespace my_book.Migrations
{
    public partial class ChangePublisherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Publishers",
                newName: "FullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Publishers",
                newName: "Name");
        }
    }
}
