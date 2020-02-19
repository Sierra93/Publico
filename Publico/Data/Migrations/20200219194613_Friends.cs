using Microsoft.EntityFrameworkCore.Migrations;

namespace Publico.Migrations
{
    public partial class Friends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "MultepleContextTable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    FriendLogin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_FriendId",
                table: "MultepleContextTable",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultepleContextTable_Friends_FriendId",
                table: "MultepleContextTable",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultepleContextTable_Friends_FriendId",
                table: "MultepleContextTable");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_MultepleContextTable_FriendId",
                table: "MultepleContextTable");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "MultepleContextTable");
        }
    }
}
