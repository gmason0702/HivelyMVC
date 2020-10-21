using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HivelyCoreMVC.Migrations
{
    public partial class FixingImageUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageUploadCreate");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Images",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Images");

            migrationBuilder.CreateTable(
                name: "ImageUploadCreate",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUploadCreate", x => x.ImageId);
                });
        }
    }
}
