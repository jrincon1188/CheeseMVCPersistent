using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CheeseMVC.Migrations
{
    public partial class AddMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Cheeses",
                newName: "CheeseID");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Cheeses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CheeseMenu",
                columns: table => new
                {
                    CheeseID = table.Column<int>(nullable: false),
                    MenuID = table.Column<int>(nullable: false),
                    CheeseMenuCheeseID = table.Column<int>(nullable: true),
                    CheeseMenuMenuID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheeseMenu", x => new { x.CheeseID, x.MenuID });
                    table.ForeignKey(
                        name: "FK_CheeseMenu_Cheeses_CheeseID",
                        column: x => x.CheeseID,
                        principalTable: "Cheeses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheeseMenu_Menus_MenuID",
                        column: x => x.MenuID,
                        principalTable: "Menus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheeseMenu_CheeseMenu_CheeseMenuCheeseID_CheeseMenuMenuID",
                        columns: x => new { x.CheeseMenuCheeseID, x.CheeseMenuMenuID },
                        principalTable: "CheeseMenu",
                        principalColumns: new[] { "CheeseID", "MenuID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cheeses_CategoryID",
                table: "Cheeses",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CheeseMenu_MenuID",
                table: "CheeseMenu",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_CheeseMenu_CheeseMenuCheeseID_CheeseMenuMenuID",
                table: "CheeseMenu",
                columns: new[] { "CheeseMenuCheeseID", "CheeseMenuMenuID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cheeses_Categories_CategoryID",
                table: "Cheeses",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheeses_Categories_CategoryID",
                table: "Cheeses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CheeseMenu");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Cheeses_CategoryID",
                table: "Cheeses");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Cheeses");

            migrationBuilder.RenameColumn(
                name: "CheeseID",
                table: "Cheeses",
                newName: "Type");
        }
    }
}
