using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonsMarketWeb.Migrations
{
    /// <inheritdoc />
    public partial class PokemonsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    power = table.Column<int>(type: "int", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
