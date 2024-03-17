using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    /// <inheritdoc />
    public partial class actorlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Actor_ActorID",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_ActorID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ActorID",
                table: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "ActorsList",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Actor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_MovieId",
                table: "Actor",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movie_MovieId",
                table: "Actor",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movie_MovieId",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_MovieId",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "ActorsList",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Actor");

            migrationBuilder.AddColumn<int>(
                name: "ActorID",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_ActorID",
                table: "Movie",
                column: "ActorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Actor_ActorID",
                table: "Movie",
                column: "ActorID",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
