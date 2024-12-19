using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yummyApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_signManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 19, 8, 24, 31, 958, DateTimeKind.Utc).AddTicks(2834),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 17, 15, 49, 217, DateTimeKind.Utc).AddTicks(9039));

            migrationBuilder.AlterColumn<string>(
                name: "ActivationCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 17, 15, 49, 217, DateTimeKind.Utc).AddTicks(9039),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 19, 8, 24, 31, 958, DateTimeKind.Utc).AddTicks(2834));

            migrationBuilder.AlterColumn<string>(
                name: "ActivationCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
