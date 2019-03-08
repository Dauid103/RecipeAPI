using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Instruction = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "ShopLists",
                columns: table => new
                {
                    ShopListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopLists", x => x.ShopListId);
                });

            migrationBuilder.CreateTable(
                name: "ShopListRecipes",
                columns: table => new
                {
                    ShopListRecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedToListDate = table.Column<DateTime>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false),
                    ShopListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopListRecipes", x => x.ShopListRecipeId);
                    table.ForeignKey(
                        name: "FK_ShopListRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopListRecipes_ShopLists_ShopListId",
                        column: x => x.ShopListId,
                        principalTable: "ShopLists",
                        principalColumn: "ShopListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopListRecipes_RecipeId",
                table: "ShopListRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopListRecipes_ShopListId",
                table: "ShopListRecipes",
                column: "ShopListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopListRecipes");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "ShopLists");
        }
    }
}
