using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemBank.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_DestinationCardNumber",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_SourceCardNumber",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_DestinationCardNumber",
                table: "Transactions",
                column: "DestinationCardNumber",
                principalTable: "Cards",
                principalColumn: "CardNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_SourceCardNumber",
                table: "Transactions",
                column: "SourceCardNumber",
                principalTable: "Cards",
                principalColumn: "CardNumber",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_DestinationCardNumber",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_SourceCardNumber",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_DestinationCardNumber",
                table: "Transactions",
                column: "DestinationCardNumber",
                principalTable: "Cards",
                principalColumn: "CardNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_SourceCardNumber",
                table: "Transactions",
                column: "SourceCardNumber",
                principalTable: "Cards",
                principalColumn: "CardNumber");
        }
    }
}
