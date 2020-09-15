using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HivelyCoreMVC.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<Guid>(nullable: false),
                    LocationName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    MapLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Queens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<Guid>(nullable: false),
                    QueenName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Color = table.Column<int>(nullable: false),
                    OriginDate = table.Column<DateTime>(nullable: false),
                    OriginLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<Guid>(nullable: false),
                    HiveName = table.Column<string>(nullable: true),
                    OriginDate = table.Column<DateTime>(nullable: false),
                    NumberOfDeeps = table.Column<int>(nullable: false),
                    HasSwarmed = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    QueenId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hives_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hives_Queens_QueenId",
                        column: x => x.QueenId,
                        principalTable: "Queens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<Guid>(nullable: false),
                    NoteTitle = table.Column<string>(nullable: true),
                    NoteDate = table.Column<DateTime>(nullable: false),
                    NoteContent = table.Column<string>(nullable: true),
                    TypeOfNote = table.Column<int>(nullable: false),
                    HiveId = table.Column<int>(nullable: true),
                    QueenId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Hives_HiveId",
                        column: x => x.HiveId,
                        principalTable: "Hives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notes_Queens_QueenId",
                        column: x => x.QueenId,
                        principalTable: "Queens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkerBees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<Guid>(nullable: false),
                    OriginDate = table.Column<DateTime>(nullable: false),
                    OriginLocation = table.Column<string>(nullable: true),
                    HiveId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerBees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerBees_Hives_HiveId",
                        column: x => x.HiveId,
                        principalTable: "Hives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hives_LocationId",
                table: "Hives",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Hives_QueenId",
                table: "Hives",
                column: "QueenId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_HiveId",
                table: "Notes",
                column: "HiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_LocationId",
                table: "Notes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_QueenId",
                table: "Notes",
                column: "QueenId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerBees_HiveId",
                table: "WorkerBees",
                column: "HiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "WorkerBees");

            migrationBuilder.DropTable(
                name: "Hives");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Queens");
        }
    }
}
