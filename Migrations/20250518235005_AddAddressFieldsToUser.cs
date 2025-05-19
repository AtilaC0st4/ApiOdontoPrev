using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdontoPrev.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressFieldsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Points = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cep = table.Column<string>(type: "NVARCHAR2(9)", maxLength: 9, nullable: true),
                    Logradouro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrushingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BrushingTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrushingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrushingRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrushingRecords_UserId",
                table: "BrushingRecords",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrushingRecords");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
