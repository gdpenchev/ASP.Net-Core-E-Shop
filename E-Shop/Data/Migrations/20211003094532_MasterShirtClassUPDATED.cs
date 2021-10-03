using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shop.Data.Migrations
{
    public partial class MasterShirtClassUPDATED : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shirts_Categories_CategoryId",
                table: "Shirts");

            migrationBuilder.DropIndex(
                name: "IX_Shirts_CategoryId",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Shirts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MasterShirts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MasterShirts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "MasterShirts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MasterShirts_CategoryId",
                table: "MasterShirts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterShirts_Categories_CategoryId",
                table: "MasterShirts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterShirts_Categories_CategoryId",
                table: "MasterShirts");

            migrationBuilder.DropIndex(
                name: "IX_MasterShirts_CategoryId",
                table: "MasterShirts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MasterShirts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MasterShirts");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "MasterShirts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Shirts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shirts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Shirts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shirts_CategoryId",
                table: "Shirts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shirts_Categories_CategoryId",
                table: "Shirts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
