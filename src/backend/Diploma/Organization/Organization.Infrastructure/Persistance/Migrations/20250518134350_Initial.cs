using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Organization.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "organization");

            migrationBuilder.CreateTable(
                name: "organization",
                schema: "organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Inn = table.Column<string>(type: "text", nullable: false),
                    LegalAddress = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    IsApproval = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.Id);
                });

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
                name: "organizationuser",
                schema: "organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationuser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_organizationuser_organization_OrganizationId",
                        column: x => x.OrganizationId,
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
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailableCount = table.Column<int>(type: "integer", nullable: false),
                    TotalSold = table.Column<int>(type: "integer", nullable: false),
                    MeasurementType = table.Column<int>(type: "integer", nullable: false),
                    IsStock = table.Column<bool>(type: "boolean", nullable: false),
                    SellOrganizationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_organization_SellOrganizationId",
                        column: x => x.SellOrganizationId,
                        principalSchema: "organization",
                        principalTable: "organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderproduct",
                schema: "organization",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_orderproduct_ProductId",
                schema: "organization",
                table: "orderproduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_organizationuser_OrganizationId",
                schema: "organization",
                table: "organizationuser",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_product_SellOrganizationId",
                schema: "organization",
                table: "product",
                column: "SellOrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderproduct",
                schema: "organization");

            migrationBuilder.DropTable(
                name: "organizationuser",
                schema: "organization");

            migrationBuilder.DropTable(
                name: "order",
                schema: "organization");

            migrationBuilder.DropTable(
                name: "product",
                schema: "organization");

            migrationBuilder.DropTable(
                name: "organization",
                schema: "organization");
        }
    }
}
