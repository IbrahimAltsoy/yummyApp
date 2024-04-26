using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yummyApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_BusinessId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Businesses_BusinessId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "OfficeID",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Tags",
                newName: "BusinessID");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_BusinessId",
                table: "Tags",
                newName: "IX_Tags_BusinessID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 26, 10, 55, 40, 772, DateTimeKind.Utc).AddTicks(332),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 26, 7, 57, 1, 620, DateTimeKind.Utc).AddTicks(2717));

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Businesses_BusinessID",
                table: "Tags",
                column: "BusinessID",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Businesses_BusinessID",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "BusinessID",
                table: "Tags",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_BusinessID",
                table: "Tags",
                newName: "IX_Tags_BusinessId");

            migrationBuilder.AddColumn<Guid>(
                name: "OfficeID",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 26, 7, 57, 1, 620, DateTimeKind.Utc).AddTicks(2717),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 26, 10, 55, 40, 772, DateTimeKind.Utc).AddTicks(332));

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Businesses_BusinessId",
                table: "Tags",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
