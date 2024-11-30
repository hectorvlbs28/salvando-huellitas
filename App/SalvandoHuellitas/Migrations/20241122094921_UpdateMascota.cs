using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalvandoHuellitas.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMascota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Adoptado",
                table: "Mascotas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adoptado",
                table: "Mascotas");
        }
    }
}
