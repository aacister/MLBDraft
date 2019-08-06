﻿// <auto-generated />
using System;
using MLBDraft.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MLBDraft.Migrations
{
    [DbContext(typeof(MLBDraftContext))]
    partial class MLBDraftContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MLBDraft.API.Entities.Draft", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid>("LeagueId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Drafts");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.DraftSelection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DraftId");

                    b.Property<Guid?>("PlayerId");

                    b.Property<string>("Round")
                        .IsRequired();

                    b.Property<string>("SelectionNo")
                        .IsRequired();

                    b.Property<Guid>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("DraftId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("DraftSelections");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.League", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaxTeams");

                    b.Property<int>("MinTeams");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.MlbTeam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("LogoPath");

                    b.HasKey("Id");

                    b.ToTable("MlbTeams");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("ImagePath");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<Guid?>("MlbTeamId");

                    b.Property<Guid?>("PositionId");

                    b.HasKey("Id");

                    b.HasIndex("MlbTeamId");

                    b.HasIndex("PositionId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.PlayerStatCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid>("StatCategoryId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("StatCategoryId");

                    b.ToTable("PlayerStatCategories");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.StatCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("StatCategories");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CatcherId");

                    b.Property<Guid?>("FirstBaseId");

                    b.Property<Guid>("LeagueId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("Outfield1Id");

                    b.Property<Guid?>("Outfield2Id");

                    b.Property<Guid?>("Outfield3Id");

                    b.Property<string>("OwnerId");

                    b.Property<Guid?>("SecondBaseId");

                    b.Property<Guid?>("ShortStopId");

                    b.Property<Guid?>("StartingPitcherId");

                    b.Property<Guid?>("ThirdBaseId");

                    b.HasKey("Id");

                    b.HasIndex("CatcherId");

                    b.HasIndex("FirstBaseId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("Outfield1Id");

                    b.HasIndex("Outfield2Id");

                    b.HasIndex("Outfield3Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SecondBaseId");

                    b.HasIndex("ShortStopId");

                    b.HasIndex("StartingPitcherId");

                    b.HasIndex("ThirdBaseId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Hash");

                    b.Property<byte[]>("Salt");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Draft", b =>
                {
                    b.HasOne("MLBDraft.API.Entities.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MLBDraft.API.Entities.DraftSelection", b =>
                {
                    b.HasOne("MLBDraft.API.Entities.Draft", "Draft")
                        .WithMany()
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("MLBDraft.API.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Player", b =>
                {
                    b.HasOne("MLBDraft.API.Entities.MlbTeam", "MlbTeam")
                        .WithMany()
                        .HasForeignKey("MlbTeamId");

                    b.HasOne("MLBDraft.API.Entities.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.PlayerStatCategory", b =>
                {
                    b.HasOne("MLBDraft.API.Entities.Player", "Player")
                        .WithMany("StatCategories")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.StatCategory", "StatCategory")
                        .WithMany("PlayerStatCategory")
                        .HasForeignKey("StatCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Team", b =>
                {
                    b.HasOne("MLBDraft.API.Entities.Player", "Catcher")
                        .WithMany()
                        .HasForeignKey("CatcherId");

                    b.HasOne("MLBDraft.API.Entities.Player", "FirstBase")
                        .WithMany()
                        .HasForeignKey("FirstBaseId");

                    b.HasOne("MLBDraft.API.Entities.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "Outfield1")
                        .WithMany()
                        .HasForeignKey("Outfield1Id");

                    b.HasOne("MLBDraft.API.Entities.Player", "Outfield2")
                        .WithMany()
                        .HasForeignKey("Outfield2Id");

                    b.HasOne("MLBDraft.API.Entities.Player", "Outfield3")
                        .WithMany()
                        .HasForeignKey("Outfield3Id");

                    b.HasOne("MLBDraft.API.Entities.User", "Owner")
                        .WithMany("Teams")
                        .HasForeignKey("OwnerId");

                    b.HasOne("MLBDraft.API.Entities.Player", "SecondBase")
                        .WithMany()
                        .HasForeignKey("SecondBaseId");

                    b.HasOne("MLBDraft.API.Entities.Player", "ShortStop")
                        .WithMany()
                        .HasForeignKey("ShortStopId");

                    b.HasOne("MLBDraft.API.Entities.Player", "StartingPitcher")
                        .WithMany()
                        .HasForeignKey("StartingPitcherId");

                    b.HasOne("MLBDraft.API.Entities.Player", "ThirdBase")
                        .WithMany()
                        .HasForeignKey("ThirdBaseId");
                });
#pragma warning restore 612, 618
        }
    }
}
