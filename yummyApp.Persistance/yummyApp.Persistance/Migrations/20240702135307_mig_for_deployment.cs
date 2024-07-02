using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yummyApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_for_deployment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 2, 13, 53, 5, 479, DateTimeKind.Utc).AddTicks(3602),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 1, 11, 45, 7, 394, DateTimeKind.Utc).AddTicks(6215));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 1, 11, 45, 7, 394, DateTimeKind.Utc).AddTicks(6215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 2, 13, 53, 5, 479, DateTimeKind.Utc).AddTicks(3602));
        }
    }
}
