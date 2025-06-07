using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organization.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_organization_Ratings_RatingId",
                schema: "organization",
                table: "organization");

            migrationBuilder.DropForeignKey(
                name: "FK_product_Ratings_RatingId",
                schema: "organization",
                table: "product");

            migrationBuilder.AlterColumn<int>(
                name: "RatingId",
                schema: "organization",
                table: "product",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RatingId",
                schema: "organization",
                table: "organization",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_organization_Ratings_RatingId",
                schema: "organization",
                table: "organization",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_Ratings_RatingId",
                schema: "organization",
                table: "product",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "RatingId",
                schema: "organization",
                table: "product",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RatingId",
                schema: "organization",
                table: "organization",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
    }
}
