using Microsoft.EntityFrameworkCore.Migrations;

namespace ITest.Migrations
{
    public partial class ChangeMailOnEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Accounts",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Accounts",
                newName: "Mail");
        }
    }
}
