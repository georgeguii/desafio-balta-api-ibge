using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Balta_IBGE.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    PasswordChangeCode = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Address = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    ActivationCode = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    ExpireDateVerifyEmail = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivateDateAccount = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
