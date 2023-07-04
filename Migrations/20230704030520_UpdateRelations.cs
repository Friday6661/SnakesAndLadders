using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnakesAndLadders.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ladders_Boards_BoardId1",
                table: "Ladders");

            migrationBuilder.DropForeignKey(
                name: "FK_Snakes_Boards_BoardId1",
                table: "Snakes");

            migrationBuilder.DropIndex(
                name: "IX_Snakes_BoardId1",
                table: "Snakes");

            migrationBuilder.DropIndex(
                name: "IX_Ladders_BoardId1",
                table: "Ladders");

            migrationBuilder.DropColumn(
                name: "BoardId1",
                table: "Snakes");

            migrationBuilder.DropColumn(
                name: "BoardId1",
                table: "Ladders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardId1",
                table: "Snakes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BoardId1",
                table: "Ladders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Snakes_BoardId1",
                table: "Snakes",
                column: "BoardId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ladders_BoardId1",
                table: "Ladders",
                column: "BoardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ladders_Boards_BoardId1",
                table: "Ladders",
                column: "BoardId1",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Snakes_Boards_BoardId1",
                table: "Snakes",
                column: "BoardId1",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
