using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpDesk.InfraStructures.DataAccess.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AspNetUsersId",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AspNetUsersId",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
