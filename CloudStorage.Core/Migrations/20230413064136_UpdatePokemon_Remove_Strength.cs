using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudStorageAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePokemon_Remove_Strength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Pokemons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Strength",
                table: "Pokemons",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
