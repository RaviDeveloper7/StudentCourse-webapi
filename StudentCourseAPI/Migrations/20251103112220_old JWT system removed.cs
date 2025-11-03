using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class oldJWTsystemremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Email", "Password", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[] { "1", "products", "xyz@gmail.com", "AQAAAAIAAYagAAAAEEGU5LGhxIgv1XM9f/PP79pGckARS5P8/qxmEhFlgqJQMwA+EpKX4+X5P9bNgmheAw==", null, null, "admin", "admin" });
        }
    }
}
