using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yummyApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class menu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 15, 11, 12, 0, 987, DateTimeKind.Utc).AddTicks(591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 8, 8, 58, 4, 860, DateTimeKind.Utc).AddTicks(8322));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 8, 8, 58, 4, 860, DateTimeKind.Utc).AddTicks(8322),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 15, 11, 12, 0, 987, DateTimeKind.Utc).AddTicks(591));
        }
    }
}
