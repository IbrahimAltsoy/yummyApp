using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yummyApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class add_Post_Title : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 27, 14, 35, 26, 805, DateTimeKind.Utc).AddTicks(6684),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 26, 10, 55, 40, 772, DateTimeKind.Utc).AddTicks(332));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 26, 10, 55, 40, 772, DateTimeKind.Utc).AddTicks(332),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 27, 14, 35, 26, 805, DateTimeKind.Utc).AddTicks(6684));
        }
    }
}
