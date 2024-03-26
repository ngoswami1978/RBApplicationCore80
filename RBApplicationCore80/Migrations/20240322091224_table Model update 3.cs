using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBApplicationCore80.Migrations
{
    /// <inheritdoc />
    public partial class tableModelupdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateName",
                table: "Employee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StateName",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
