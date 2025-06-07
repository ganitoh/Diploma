using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organization.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class finalFixRating : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_RatingCommentary_Ratings_RatingId",
                table: "RatingCommentary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatingCommentary",
                table: "RatingCommentary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "RatingCommentary",
                newName: "ratingcommentary",
                newSchema: "organization");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "rating",
                newSchema: "organization");

            migrationBuilder.RenameIndex(
                name: "IX_RatingCommentary_RatingId",
                schema: "organization",
                table: "ratingcommentary",
                newName: "IX_ratingcommentary_RatingId");

            migrationBuilder.AlterColumn<int>(
                name: "RatingId",
                schema: "organization",
                table: "ratingcommentary",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Commentary",
                schema: "organization",
                table: "ratingcommentary",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ratingcommentary",
                schema: "organization",
                table: "ratingcommentary",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rating",
                schema: "organization",
                table: "rating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_organization_rating_RatingId",
                schema: "organization",
                table: "organization",
                column: "RatingId",
                principalSchema: "organization",
                principalTable: "rating",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_rating_RatingId",
                schema: "organization",
                table: "product",
                column: "RatingId",
                principalSchema: "organization",
                principalTable: "rating",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ratingcommentary_rating_RatingId",
                schema: "organization",
                table: "ratingcommentary",
                column: "RatingId",
                principalSchema: "organization",
                principalTable: "rating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_organization_rating_RatingId",
                schema: "organization",
                table: "organization");

            migrationBuilder.DropForeignKey(
                name: "FK_product_rating_RatingId",
                schema: "organization",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_ratingcommentary_rating_RatingId",
                schema: "organization",
                table: "ratingcommentary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ratingcommentary",
                schema: "organization",
                table: "ratingcommentary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rating",
                schema: "organization",
                table: "rating");

            migrationBuilder.RenameTable(
                name: "ratingcommentary",
                schema: "organization",
                newName: "RatingCommentary");

            migrationBuilder.RenameTable(
                name: "rating",
                schema: "organization",
                newName: "Ratings");

            migrationBuilder.RenameIndex(
                name: "IX_ratingcommentary_RatingId",
                table: "RatingCommentary",
                newName: "IX_RatingCommentary_RatingId");

            migrationBuilder.AlterColumn<int>(
                name: "RatingId",
                table: "RatingCommentary",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Commentary",
                table: "RatingCommentary",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatingCommentary",
                table: "RatingCommentary",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RatingCommentary_Ratings_RatingId",
                table: "RatingCommentary",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");
        }
    }
}
