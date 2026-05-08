using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManage.Migrations
{
    /// <inheritdoc />
    public partial class RenamedEmployeeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                table: "users",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "EmployeeName");
        }
    }
}
