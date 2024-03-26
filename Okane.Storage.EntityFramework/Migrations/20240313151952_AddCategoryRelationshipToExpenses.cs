using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Okane.Storage.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryRelationshipToExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Category", table: "Expenses", newName: "CategoryName");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", 
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
            
            migrationBuilder.Sql(@"
                INSERT INTO public.""Categories"" (""Name"") 
                SELECT DISTINCT ""CategoryName"" FROM ""Expenses""
            ");
            
            migrationBuilder.Sql(@"
                UPDATE ""Expenses"" AS e
                SET ""CategoryId"" = c.""Id""
                FROM ""Categories"" AS c
                WHERE e.""CategoryName"" = c.""Name"";
            ");
            
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Expenses");
            
            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Expenses",
                type: "text",
                nullable: true,
                defaultValue: "");
            
            migrationBuilder.Sql(@"
                UPDATE ""Expenses"" AS e
                SET ""Category"" = c.""Name""
                FROM ""Categories"" AS c
                WHERE e.""CategoryId"" = c.""Id"";
            ");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Expenses");
        }
    }
}
