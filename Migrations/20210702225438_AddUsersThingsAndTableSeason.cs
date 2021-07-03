using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WardbApp.Migrations
{
    public partial class AddUsersThingsAndTableSeason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeasonClothing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonClothing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersThings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    Image = table.Column<byte[]>(type: "longblob", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersThings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersThings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersThings_CategoryClothing_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryClothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersThings_SeasonClothing_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "SeasonClothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersThings_CategoryId",
                table: "UsersThings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersThings_SeasonId",
                table: "UsersThings",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersThings_UserId",
                table: "UsersThings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersThings");

            migrationBuilder.DropTable(
                name: "SeasonClothing");
        }
    }
}
