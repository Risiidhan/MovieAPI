using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainActor",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "releasedDate",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "isMyFavourite",
                table: "Movie",
                newName: "IsMyFavourite");

            migrationBuilder.AddColumn<int>(
                name: "ActorID",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReleasedYear",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Actor_ActorID",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Movie_ActorID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ActorID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ReleasedYear",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "IsMyFavourite",
                table: "Movie",
                newName: "isMyFavourite");

            migrationBuilder.AddColumn<string>(
                name: "MainActor",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "releasedDate",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
