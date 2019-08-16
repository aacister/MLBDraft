using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MLBDraft.Migrations
{
    public partial class mlbDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "MlbTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Abbreviation = table.Column<string>(nullable: true),
                    LogoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MlbTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Abbreviation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Abbreviation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeagueId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drafts_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    MlbTeamId = table.Column<Guid>(nullable: true),
                    PositionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_MlbTeams_MlbTeamId",
                        column: x => x.MlbTeamId,
                        principalTable: "MlbTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    StatCategoryId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatCategories_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerStatCategories_StatCategories_StatCategoryId",
                        column: x => x.StatCategoryId,
                        principalTable: "StatCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    LeagueId = table.Column<Guid>(nullable: true),
                    CatcherId = table.Column<Guid>(nullable: true),
                    FirstBaseId = table.Column<Guid>(nullable: true),
                    SecondBaseId = table.Column<Guid>(nullable: true),
                    ThirdBaseId = table.Column<Guid>(nullable: true),
                    ShortStopId = table.Column<Guid>(nullable: true),
                    Outfield1Id = table.Column<Guid>(nullable: true),
                    Outfield2Id = table.Column<Guid>(nullable: true),
                    Outfield3Id = table.Column<Guid>(nullable: true),
                    StartingPitcherId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Players_CatcherId",
                        column: x => x.CatcherId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_FirstBaseId",
                        column: x => x.FirstBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_Outfield1Id",
                        column: x => x.Outfield1Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_Outfield2Id",
                        column: x => x.Outfield2Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_Outfield3Id",
                        column: x => x.Outfield3Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_SecondBaseId",
                        column: x => x.SecondBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_ShortStopId",
                        column: x => x.ShortStopId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_StartingPitcherId",
                        column: x => x.StartingPitcherId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Players_ThirdBaseId",
                        column: x => x.ThirdBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DraftSelections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DraftId = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: true),
                    Round = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftSelections_Drafts_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Drafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DraftSelections_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftSelections_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DraftTeamRosters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DraftId = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    CatcherId = table.Column<Guid>(nullable: true),
                    FirstBaseId = table.Column<Guid>(nullable: true),
                    SecondBaseId = table.Column<Guid>(nullable: true),
                    ThirdBaseId = table.Column<Guid>(nullable: true),
                    ShortStopId = table.Column<Guid>(nullable: true),
                    Outfield1Id = table.Column<Guid>(nullable: true),
                    Outfield2Id = table.Column<Guid>(nullable: true),
                    Outfield3Id = table.Column<Guid>(nullable: true),
                    StartingPitcherId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftTeamRosters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_CatcherId",
                        column: x => x.CatcherId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Drafts_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Drafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_FirstBaseId",
                        column: x => x.FirstBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_Outfield1Id",
                        column: x => x.Outfield1Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_Outfield2Id",
                        column: x => x.Outfield2Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_Outfield3Id",
                        column: x => x.Outfield3Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_SecondBaseId",
                        column: x => x.SecondBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_ShortStopId",
                        column: x => x.ShortStopId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_StartingPitcherId",
                        column: x => x.StartingPitcherId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DraftTeamRosters_Players_ThirdBaseId",
                        column: x => x.ThirdBaseId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_LeagueId",
                table: "Drafts",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftSelections_DraftId",
                table: "DraftSelections",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftSelections_PlayerId",
                table: "DraftSelections",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftSelections_TeamId",
                table: "DraftSelections",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_CatcherId",
                table: "DraftTeamRosters",
                column: "CatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_DraftId",
                table: "DraftTeamRosters",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_FirstBaseId",
                table: "DraftTeamRosters",
                column: "FirstBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_Outfield1Id",
                table: "DraftTeamRosters",
                column: "Outfield1Id");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_Outfield2Id",
                table: "DraftTeamRosters",
                column: "Outfield2Id");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_Outfield3Id",
                table: "DraftTeamRosters",
                column: "Outfield3Id");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_SecondBaseId",
                table: "DraftTeamRosters",
                column: "SecondBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_ShortStopId",
                table: "DraftTeamRosters",
                column: "ShortStopId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_StartingPitcherId",
                table: "DraftTeamRosters",
                column: "StartingPitcherId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_TeamId",
                table: "DraftTeamRosters",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftTeamRosters_ThirdBaseId",
                table: "DraftTeamRosters",
                column: "ThirdBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_MlbTeamId",
                table: "Players",
                column: "MlbTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PositionId",
                table: "Players",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatCategories_PlayerId",
                table: "PlayerStatCategories",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatCategories_StatCategoryId",
                table: "PlayerStatCategories",
                column: "StatCategoryId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DraftSelections");

            migrationBuilder.DropTable(
                name: "DraftTeamRosters");

            migrationBuilder.DropTable(
                name: "PlayerStatCategories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "StatCategories");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MlbTeams");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
