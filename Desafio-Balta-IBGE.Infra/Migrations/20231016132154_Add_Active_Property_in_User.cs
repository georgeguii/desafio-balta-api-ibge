using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Balta_IBGE.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Add_Active_Property_in_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "User");
        }
    }
}
