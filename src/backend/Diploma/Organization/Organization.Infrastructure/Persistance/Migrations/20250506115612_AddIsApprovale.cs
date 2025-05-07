using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organization.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApprovale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproval",
                schema: "organization",
                table: "organization",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproval",
                schema: "organization",
                table: "organization");
        }
    }
}
