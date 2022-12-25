using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonsMarketWeb.Migrations
{
    /// <inheritdoc />
    public partial class addmigratonaddSepeciesPokemonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pokemonSpecies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    detailColor = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "blue"),
                    rarity = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Common")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pokemonSpecies", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pokemonSpecies");
        }
    }
}
