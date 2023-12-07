using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudStorageAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Strength",
                table: "Pokemons",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Pokemons");
        }
    }
}
