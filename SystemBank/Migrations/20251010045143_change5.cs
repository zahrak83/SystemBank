using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemBank.Migrations
{
    /// <inheritdoc />
    public partial class change5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuccesful",
                table: "Transactions",
                newName: "IsSuccessful");

            migrationBuilder.AlterColumn<float>(
                name: "Fee",
                table: "Transactions",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuccessful",
                table: "Transactions",
                newName: "IsSuccesful");

            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
