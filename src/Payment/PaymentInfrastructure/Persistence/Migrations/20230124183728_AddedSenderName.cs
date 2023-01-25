using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentInfrastructure.Persistence.Migrations
{
    public partial class AddedSenderName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Transactions");
        }
    }
}
