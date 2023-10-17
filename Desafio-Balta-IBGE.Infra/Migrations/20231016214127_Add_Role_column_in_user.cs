using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Balta_IBGE.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Add_Role_column_in_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "User",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Administrador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
