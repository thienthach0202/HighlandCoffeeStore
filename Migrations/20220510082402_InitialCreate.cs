using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeStore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coffees",
                columns: table => new
                {
                    CoffeeID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coffees", x => x.CoffeeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coffees");
        }
    }
}
