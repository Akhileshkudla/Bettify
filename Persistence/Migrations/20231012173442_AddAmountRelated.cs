using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChosenOption",
                table: "ActivityAttendees",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmountIfLose",
                table: "Activities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountIfWon",
                table: "Activities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMandatoryActivity",
                table: "Activities",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WinningOption",
                table: "Activities",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChosenOption",
                table: "ActivityAttendees");

            migrationBuilder.DropColumn(
                name: "AmountIfLose",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AmountIfWon",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "IsMandatoryActivity",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "WinningOption",
                table: "Activities");
        }
    }
}
