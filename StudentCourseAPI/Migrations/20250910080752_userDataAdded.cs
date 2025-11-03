using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class userDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Email", "Password", "Role", "Username" },
                values: new object[] { "1", "products", "xyz@gmail.com", "admin", "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
