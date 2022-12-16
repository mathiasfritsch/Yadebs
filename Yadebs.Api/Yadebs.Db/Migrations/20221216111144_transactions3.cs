using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yadebs.Db.Migrations
{
    public partial class transactions3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDebit",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDebit",
                table: "Transactions");
        }
    }
}
