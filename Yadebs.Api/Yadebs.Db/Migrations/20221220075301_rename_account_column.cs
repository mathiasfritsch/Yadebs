using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yadebs.Db.Migrations
{
    public partial class rename_account_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncreasesWhenMoneyAdded",
                table: "Accounts",
                newName: "IncreasesDebitWhenMoneyAdded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncreasesDebitWhenMoneyAdded",
                table: "Accounts",
                newName: "IncreasesWhenMoneyAdded");
        }
    }
}
