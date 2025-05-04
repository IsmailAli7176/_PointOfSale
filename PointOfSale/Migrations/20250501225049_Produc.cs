using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Migrations
{
    /// <inheritdoc />
    public partial class Produc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Product_Quantity",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Product_Total_Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product_Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Product_Total_Price",
                table: "Products");
        }
    }
}
