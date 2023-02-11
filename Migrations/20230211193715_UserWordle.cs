using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wordleboard.Migrations
{
    public partial class UserWordle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaysCount",
                table: "UserBoards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserWordles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWordles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWordles");

            migrationBuilder.DropColumn(
                name: "DaysCount",
                table: "UserBoards");
        }
    }
}
