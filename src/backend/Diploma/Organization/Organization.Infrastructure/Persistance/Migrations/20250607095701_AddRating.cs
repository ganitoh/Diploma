using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Organization.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                schema: "organization",
                table: "product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                schema: "organization",
                table: "organization",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Vale = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingCommentary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RatingValue = table.Column<decimal>(type: "numeric", nullable: false),
                    Commentary = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RatingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingCommentary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingCommentary_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_RatingId",
                schema: "organization",
                table: "product",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_organization_RatingId",
                schema: "organization",
                table: "organization",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingCommentary_RatingId",
                table: "RatingCommentary",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_organization_Ratings_RatingId",
                schema: "organization",
                table: "organization",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_Ratings_RatingId",
                schema: "organization",
                table: "product",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_organization_Ratings_RatingId",
                schema: "organization",
                table: "organization");

            migrationBuilder.DropForeignKey(
                name: "FK_product_Ratings_RatingId",
                schema: "organization",
                table: "product");

            migrationBuilder.DropTable(
                name: "RatingCommentary");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_product_RatingId",
                schema: "organization",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_organization_RatingId",
                schema: "organization",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "RatingId",
                schema: "organization",
                table: "product");

            migrationBuilder.DropColumn(
                name: "RatingId",
                schema: "organization",
                table: "organization");
        }
    }
}
