using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Market.Infrastructure.Persistence.Migrations
{
    public partial class UpdateTableAdsProperies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");
        }
    }
}
