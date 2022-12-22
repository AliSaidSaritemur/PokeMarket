using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonsMarketWeb.Migrations
{
    /// <inheritdoc />
    public partial class Sellig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sellStatue",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "selling");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sellStatue",
                table: "Pokemons");
        }
    }
}
