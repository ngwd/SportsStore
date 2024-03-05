using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, Category) VALUES ('Football','fussball', 22.00, '')");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, Category) VALUES ('Surfboard','surf board', 25.00, '')");
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, Category) VALUES ('RunningShoes','running shoes', 25.00, '')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
