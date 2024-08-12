using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WS.Migrations
{
    /// <inheritdoc />
    public partial class addDiasNoExame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dias",
                table: "Exames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dias",
                table: "Exames");
        }
    }
}
