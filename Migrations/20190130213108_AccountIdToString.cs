using Microsoft.EntityFrameworkCore.Migrations;

namespace SecondTask.Migrations
{
    public partial class AccountIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Persons",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AccountId",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
