using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organization.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ReworkOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderproduct",
                schema: "organization");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "organization",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "organization",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_order_ProductId",
                schema: "organization",
                table: "order",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_ProductId",
                schema: "organization",
                table: "order",
                column: "ProductId",
                principalSchema: "organization",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_product_ProductId",
                schema: "organization",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_ProductId",
                schema: "organization",
                table: "order");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "organization",
                table: "order");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "organization",
                table: "order");

            migrationBuilder.CreateTable(
                name: "orderproduct",
                schema: "organization",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderproduct", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_orderproduct_order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "organization",
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderproduct_product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "organization",
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderproduct_ProductId",
                schema: "organization",
                table: "orderproduct",
                column: "ProductId");
        }
    }
}
