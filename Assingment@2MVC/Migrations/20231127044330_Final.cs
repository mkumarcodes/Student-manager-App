using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assingment_2MVC.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2023, 11, 26, 23, 43, 30, 396, DateTimeKind.Local).AddTicks(3954));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2023, 11, 24, 23, 48, 0, 149, DateTimeKind.Local).AddTicks(7592));
        }
    }
}
