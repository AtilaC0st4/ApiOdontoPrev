using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdontoPrev.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarBrushingRecordAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "BrushingRecords");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BrushingRecords",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BrushingRecords_UserId",
                table: "BrushingRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrushingRecords_Users_UserId",
                table: "BrushingRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrushingRecords_Users_UserId",
                table: "BrushingRecords");

            migrationBuilder.DropIndex(
                name: "IX_BrushingRecords_UserId",
                table: "BrushingRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BrushingRecords");

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "BrushingRecords",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
