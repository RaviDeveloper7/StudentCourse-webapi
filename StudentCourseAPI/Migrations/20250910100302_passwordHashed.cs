using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class passwordHashed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1",
                column: "Password",
                value: "AQAAAAIAAYagAAAAEEGU5LGhxIgv1XM9f/PP79pGckARS5P8/qxmEhFlgqJQMwA+EpKX4+X5P9bNgmheAw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1",
                column: "Password",
                value: "AQAAAAIAAYagAAAAEB5ztMbBbZc4KsshVlMO1PhD+bGdb8ZCE8V2dESb9/WPackVN3gCuljc7LImGq+OYA==");
        }
    }
}
