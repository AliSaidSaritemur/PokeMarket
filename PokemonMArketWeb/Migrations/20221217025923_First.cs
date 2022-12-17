using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonsMarketWeb.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
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
                    price = table.Column<int>(type: "int", nullable: false, defaultValue: 300),
                    UserId = table.Column<int>(type: "int", nullable: false, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false, defaultValue: "5000"),
                    mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    wallet = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_mail",
                table: "Users",
                column: "mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_phone",
                table: "Users",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_userName",
                table: "Users",
                column: "userName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
