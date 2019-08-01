using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MLBDraft.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder){
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    MinTeams = table.Column<int>(nullable: false),
                    MaxTeams = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    Team = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    BattingAverage = table.Column<string>(nullable: false),
                    HomeRuns = table.Column<string>(nullable: false),
                    Runs = table.Column<string>(nullable: false),
                    RunsBattedIn = table.Column<string>(nullable: false),
                    StolenBases = table.Column<string>(nullable: false),
                    InningsPitched = table.Column<string>(nullable: false),
                    Wins = table.Column<string>(nullable: false),
                    Strikeouts = table.Column<string>(nullable: false),
                    EarnedRunAverage = table.Column<string>(nullable: false),
                    WHIP = table.Column<string>(nullable: false)
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false),
                    LeagueId = table.Column<Guid>(nullable: false),
                    CatcherId = table.Column<Guid>(nullable: false),
                    FirstBaseId = table.Column<Guid>(nullable: false),
                    SecondBaseId = table.Column<Guid>(nullable: false),
                    ThirdBaseId = table.Column<Guid>(nullable: false),
                    ShortStopId = table.Column<Guid>(nullable: false),
                    Outfield1Id = table.Column<Guid>(nullable: false),
                    Outfield2Id = table.Column<Guid>(nullable: false),
                    Outfield3Id = table.Column<Guid>(nullable: false),
                    StartingPitcherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Players_CatcherId",
                        column: x => x.CatcherId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Players_FirstBaseId",
                        column: x => x.FirstBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Players_Outfield1Id",
                        column: x => x.Outfield1Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Players_Outfield2Id",
                        column: x => x.Outfield2Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Players_Outfield3Id",
                        column: x => x.Outfield3Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Players_SecondBaseId",
                        column: x => x.SecondBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Players_ShortStopId",
                        column: x => x.ShortStopId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Players_StartingPitcherId",
                        column: x => x.StartingPitcherId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Players_ThirdBaseId",
                        column: x => x.ThirdBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SelectionNo = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drafts_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drafts_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_PlayerId",
                table: "Drafts",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_TeamId",
                table: "Drafts",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CatcherId",
                table: "Teams",
                column: "CatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FirstBaseId",
                table: "Teams",
                column: "FirstBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Outfield1Id",
                table: "Teams",
                column: "Outfield1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Outfield2Id",
                table: "Teams",
                column: "Outfield2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Outfield3Id",
                table: "Teams",
                column: "Outfield3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId",
                table: "Teams",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SecondBaseId",
                table: "Teams",
                column: "SecondBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ShortStopId",
                table: "Teams",
                column: "ShortStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StartingPitcherId",
                table: "Teams",
                column: "StartingPitcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ThirdBaseId",
                table: "Teams",
                column: "ThirdBaseId");
        }

         protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}