using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Balta_IBGE.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ibge",
                columns: table => new
                {
                    IbgeId = table.Column<string>(type: "char(7)", nullable: false),
                    City = table.Column<string>(type: "varchar(80)", nullable: false),
                    State = table.Column<string>(type: "char(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ibge", x => x.IbgeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ibge");
        }
    }
}
