using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shop.Data.Migrations
{
    public partial class MasterShirtClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shirts");

            migrationBuilder.AddColumn<int>(
                name: "MasterShirtId",
                table: "Shirts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Shirts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MasterShirts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterShirts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shirts_MasterShirtId",
                table: "Shirts",
                column: "MasterShirtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shirts_MasterShirts_MasterShirtId",
                table: "Shirts",
                column: "MasterShirtId",
                principalTable: "MasterShirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shirts_MasterShirts_MasterShirtId",
                table: "Shirts");

            migrationBuilder.DropTable(
                name: "MasterShirts");

            migrationBuilder.DropIndex(
                name: "IX_Shirts_MasterShirtId",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "MasterShirtId",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Shirts");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Shirts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shirts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lenghth = table.Column<double>(type: "float", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                });
        }
    }
}
