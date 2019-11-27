using Microsoft.EntityFrameworkCore.Migrations;

namespace Brewsy.Data.Migrations
{
    public partial class Removestripeproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeAccessToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StripePublishableKey",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StripeRefreshToken",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeAccessToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripePublishableKey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeRefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
