using System;
using MLBDraft.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MLBDraft.API.Migrations
{
    [DbContext(typeof(MLBDraftContext))]
    [Migration("20190730134500_InitialCreate")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {

            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MLBDraft.API.Entities.Draft", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("SelectionNo");

                    b.Property<Guid>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Drafts");
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

            modelBuilder.Entity("MLBDraft.API.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("ImagePath")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Position")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CatcherId");

                    b.Property<Guid>("FirstBaseId");

                    b.Property<Guid>("LeagueId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("Outfield1Id");

                    b.Property<Guid>("Outfield2Id");

                    b.Property<Guid>("Outfield3Id");

                    b.Property<Guid>("OwnerId");

                    b.Property<Guid>("SecondBaseId");

                    b.Property<Guid>("ShortStopId");

                    b.Property<Guid>("StartingPitcherId");

                    b.Property<Guid>("ThirdBaseId");

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
                        .IsRequired();
                    
                    b.Property<byte[]>("Hash");

                    b.Property<byte[]>("Salt");

                    b.HasKey("Username");
                    

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Draft", b =>
                {
                    b.HasOne("MLBDraft.API.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MLBDraft.API.Entities.Team", b =>
                {
                    b.HasOne("MLBDraft.API.Entities.Player", "Catcher")
                        .WithMany()
                        .HasForeignKey("CatcherId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "FirstBase")
                        .WithMany()
                        .HasForeignKey("FirstBaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "Outfield1")
                        .WithMany()
                        .HasForeignKey("Outfield1Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "Outfield2")
                        .WithMany()
                        .HasForeignKey("Outfield2Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "Outfield3")
                        .WithMany()
                        .HasForeignKey("Outfield3Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "SecondBase")
                        .WithMany()
                        .HasForeignKey("SecondBaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "ShortStop")
                        .WithMany()
                        .HasForeignKey("ShortStopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "StartingPitcher")
                        .WithMany()
                        .HasForeignKey("StartingPitcherId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MLBDraft.API.Entities.Player", "ThirdBase")
                        .WithMany()
                        .HasForeignKey("ThirdBaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

        }
    }
}
