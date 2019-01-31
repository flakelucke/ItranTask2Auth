using Microsoft.EntityFrameworkCore.Migrations;

namespace SecondTask.Migrations
{
    public partial class AddLongTypeToAccountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AccountId",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
