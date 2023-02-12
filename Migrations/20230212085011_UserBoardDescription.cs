using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wordleboard.Migrations
{
    public partial class UserBoardDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoardDescription",
                table: "UserBoards",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardDescription",
                table: "UserBoards");
        }
    }
}
