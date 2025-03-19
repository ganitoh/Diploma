using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Organization.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitMainBuisnesLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "organization",
                table: "organization",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "INN",
                schema: "organization",
                table: "organization",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LegalAddress",
                schema: "organization",
                table: "organization",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "order",
                schema: "organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SellerOrganizationId = table.Column<int>(type: "integer", nullable: false),
                    BuyerOrganizationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_organization_BuyerOrganizationId",
                        column: x => x.BuyerOrganizationId,
                        principalSchema: "organization",
                        principalTable: "organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_organization_SellerOrganizationId",
                        column: x => x.SellerOrganizationId,
                        principalSchema: "organization",
                        principalTable: "organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailableCount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalSold = table.Column<decimal>(type: "numeric", nullable: false),
                    MeasurementType = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsStock = table.Column<bool>(type: "boolean", nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "organization",
                        principalTable: "organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                schema: "organization",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductOrders_order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "organization",
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "organization",
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_BuyerOrganizationId",
                schema: "organization",
                table: "order",
                column: "BuyerOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_order_SellerOrganizationId",
                schema: "organization",
                table: "order",
                column: "SellerOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_product_OrganizationId",
                schema: "organization",
                table: "product",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductId",
                schema: "organization",
                table: "ProductOrders",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrders",
                schema: "organization");

            migrationBuilder.DropTable(
                name: "order",
                schema: "organization");

            migrationBuilder.DropTable(
                name: "product",
                schema: "organization");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "organization",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "INN",
                schema: "organization",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "LegalAddress",
                schema: "organization",
                table: "organization");
        }
    }
}
