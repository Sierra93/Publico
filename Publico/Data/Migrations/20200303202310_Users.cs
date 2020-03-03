using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Publico.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Info",
                columns: table => new
                {
                    InfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdTo = table.Column<int>(nullable: false),
                    UserIdFrom = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info", x => x.InfoId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserId = table.Column<int>(nullable: false),
                    ToUserId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTranslate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTranslate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    AvatarUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersRelations",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToUserId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRelations", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "MultepleContextTable",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<int>(nullable: false),
                    InfoId = table.Column<int>(nullable: false),
                    MessageId = table.Column<int>(nullable: false),
                    MessageTranId = table.Column<int>(nullable: false),
                    UsersRelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultepleContextTable", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_MultepleContextTable_Info_InfoId",
                        column: x => x.InfoId,
                        principalTable: "Info",
                        principalColumn: "InfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultepleContextTable_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultepleContextTable_MessageTranslate_MessageTranId",
                        column: x => x.MessageTranId,
                        principalTable: "MessageTranslate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultepleContextTable_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultepleContextTable_UsersRelations_UsersRelId",
                        column: x => x.UsersRelId,
                        principalTable: "UsersRelations",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_InfoId",
                table: "MultepleContextTable",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_MessageId",
                table: "MultepleContextTable",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_MessageTranId",
                table: "MultepleContextTable",
                column: "MessageTranId");

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_UserId1",
                table: "MultepleContextTable",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_MultepleContextTable_UsersRelId",
                table: "MultepleContextTable",
                column: "UsersRelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultepleContextTable");

            migrationBuilder.DropTable(
                name: "Info");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MessageTranslate");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UsersRelations");
        }
    }
}
