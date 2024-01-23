﻿// <auto-generated />
using System;
using BotTournamentManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BotTournamentManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240122024132_%3")]
    partial class _3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.HighSchoolEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("HighSchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "KeyId" }, "Index_KeyId")
                        .IsUnique();

                    b.ToTable("HighSchool");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.MapEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("KeyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MapName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "KeyId" }, "Index_KeyId")
                        .IsUnique()
                        .HasDatabaseName("Index_KeyId1");

                    b.ToTable("Map");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.MatchEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MapId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("MatchDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("RoundId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TournamentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.HasIndex("RoundId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.RoundEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("RoundName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Round");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.TeamEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("HighSchoolId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("KeyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HighSchoolId");

                    b.HasIndex(new[] { "KeyId" }, "Index_KeyId")
                        .IsUnique()
                        .HasDatabaseName("Index_KeyId2");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.TeamResultEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MatchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isWinner")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamResult");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.TournamentEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("KeyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("TournamentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "KeyId" }, "Index_KeyId")
                        .IsUnique()
                        .HasDatabaseName("Index_KeyId3");

                    b.ToTable("Tournament");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.MatchEntity", b =>
                {
                    b.HasOne("BotTournamentManagement.Data.Entities.MapEntity", "Map")
                        .WithMany()
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotTournamentManagement.Data.Entities.RoundEntity", "Round")
                        .WithMany("Matches")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotTournamentManagement.Data.Entities.TournamentEntity", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Map");

                    b.Navigation("Round");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.TeamEntity", b =>
                {
                    b.HasOne("BotTournamentManagement.Data.Entities.HighSchoolEntity", "HighSchool")
                        .WithMany("Teams")
                        .HasForeignKey("HighSchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HighSchool");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.TeamResultEntity", b =>
                {
                    b.HasOne("BotTournamentManagement.Data.Entities.MatchEntity", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotTournamentManagement.Data.Entities.TeamEntity", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.HighSchoolEntity", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.RoundEntity", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("BotTournamentManagement.Data.Entities.TournamentEntity", b =>
                {
                    b.Navigation("Matches");
                });
#pragma warning restore 612, 618
        }
    }
}
