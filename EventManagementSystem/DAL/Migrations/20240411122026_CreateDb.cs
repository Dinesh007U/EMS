using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventDetails",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetails", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "SpeakersDetails",
                columns: table => new
                {
                    SpeakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpeakerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakersDetails", x => x.SpeakerId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    EmailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "SessionInfo",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SpeakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionInfo", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_SessionInfo_EventDetails_EventId",
                        column: x => x.EventId,
                        principalTable: "EventDetails",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionInfo_SpeakersDetails_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "SpeakersDetails",
                        principalColumn: "SpeakerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participantEventDetils",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    participantEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAttended = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participantEventDetils", x => x.ID);
                    table.ForeignKey(
                        name: "FK_participantEventDetils_EventDetails_EventId",
                        column: x => x.EventId,
                        principalTable: "EventDetails",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participantEventDetils_UserInfo_participantEmail",
                        column: x => x.participantEmail,
                        principalTable: "UserInfo",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "EmailId", "IsDeleted", "Password", "Role", "UserName" },
                values: new object[] { "admin@gmail.com", false, "admin", "Admin", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_participantEventDetils_EventId",
                table: "participantEventDetils",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_participantEventDetils_participantEmail",
                table: "participantEventDetils",
                column: "participantEmail");

            migrationBuilder.CreateIndex(
                name: "IX_SessionInfo_EventId",
                table: "SessionInfo",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionInfo_SpeakerId",
                table: "SessionInfo",
                column: "SpeakerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participantEventDetils");

            migrationBuilder.DropTable(
                name: "SessionInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "EventDetails");

            migrationBuilder.DropTable(
                name: "SpeakersDetails");
        }
    }
}
