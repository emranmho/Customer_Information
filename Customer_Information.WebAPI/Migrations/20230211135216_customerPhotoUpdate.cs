using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer_Information.WebAPI.Migrations
{
    public partial class customerPhotoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "CustomerPhoto",
                table: "Customers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "CustomerPhoto",
                table: "Customers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
