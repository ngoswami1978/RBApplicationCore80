using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBApplicationCore80.Migrations
{
    /// <inheritdoc />
    public partial class tableModelupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Employee");
        }
    }
}
