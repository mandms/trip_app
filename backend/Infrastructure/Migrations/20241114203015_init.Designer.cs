﻿// <auto-generated />
using System;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(TripAppDbContext))]
    [Migration("20241114203015_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "Availability description",
                            Name = "Availability"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "Interests description",
                            Name = "Interests"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Road description",
                            Name = "Road"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ImageLocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<long>("LocationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("ImageLocation", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Image = "Image1.jpg",
                            LocationId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Image = "Image2.jpg",
                            LocationId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            Image = "Image3.jpg",
                            LocationId = 3L
                        });
                });

            modelBuilder.Entity("Domain.Entities.ImageMoment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<long>("MomentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MomentId");

                    b.ToTable("ImageMoment", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Image = "Image1.jpg",
                            MomentId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Image = "Image2.jpg",
                            MomentId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            Image = "Image3.jpg",
                            MomentId = 3L
                        });
                });

            modelBuilder.Entity("Domain.Entities.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Point>("Coordinates")
                        .IsRequired()
                        .HasColumnType("geography (point)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<long>("RouteId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Location", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Coordinates = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (55.877806802128674 37.560213608843085)"),
                            Description = "Location description 1",
                            Name = "Moscow",
                            Order = 1,
                            RouteId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Coordinates = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (50.62478102741532 86.32222662409758)"),
                            Description = "Location description 2",
                            Name = "Altai",
                            Order = 1,
                            RouteId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            Coordinates = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (48.8615359552553 2.358705469822234)"),
                            Description = "Location description 2",
                            Name = "Paris",
                            Order = 1,
                            RouteId = 3L
                        });
                });

            modelBuilder.Entity("Domain.Entities.Moment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Point>("Coordinates")
                        .IsRequired()
                        .HasColumnType("geography (point)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Moment", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Coordinates = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (48.858779767208894 2.294590215790281)"),
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1301),
                            Description = "Moment description 1",
                            Status = 4,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Coordinates = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (55.751165864532894 37.61726058361952)"),
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1305),
                            Description = "Moment description 2",
                            Status = 5,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            Coordinates = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (50.04278538093594 87.40137428089643)"),
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1309),
                            Description = "Moment description 3",
                            Status = 3,
                            UserId = 3L
                        });
                });

            modelBuilder.Entity("Domain.Entities.Note", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("RouteId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Note", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1160),
                            RouteId = 1L,
                            Text = "Text 1"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1162),
                            RouteId = 2L,
                            Text = "Text 2"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1163),
                            RouteId = 3L,
                            Text = "Text 3"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rate")
                        .HasColumnType("integer");

                    b.Property<long>("RouteId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("Review", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1205),
                            Rate = 5,
                            RouteId = 3L,
                            Text = "Text 1",
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1221),
                            Rate = 4,
                            RouteId = 1L,
                            Text = "Text 2",
                            UserId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2024, 11, 14, 20, 30, 14, 431, DateTimeKind.Utc).AddTicks(1223),
                            Rate = 3,
                            RouteId = 2L,
                            Text = "Text 3",
                            UserId = 3L
                        });
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2L,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Superuser"
                        },
                        new
                        {
                            Id = 1L,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Route", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("Duration")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Route", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "Route description 1",
                            Duration = 1,
                            Name = "Moscow - Saint Petersburg",
                            Status = 1,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Description = "Route description 2",
                            Duration = 10,
                            Name = "Mountain tour",
                            Status = 1,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Route description 3",
                            Duration = 20,
                            Name = "European travel",
                            Status = 0,
                            UserId = 3L
                        });
                });

            modelBuilder.Entity("Domain.Entities.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Tag", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CategoryId = 1L,
                            Name = "Tourist"
                        },
                        new
                        {
                            Id = 2L,
                            CategoryId = 3L,
                            Name = "Bad road"
                        },
                        new
                        {
                            Id = 3L,
                            CategoryId = 2L,
                            Name = "Diving"
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasDefaultValue("user-default.png");

                    b.Property<string>("Biography")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Avatar = "avatar1.jpg",
                            Biography = "Biography1..",
                            Email = "one@mail.com",
                            Password = "$2a$11$SmyD2YXfXryrvFHyz2C64ehbmfT0D7xCRGzmlFfdltkYmLdS1pnx.",
                            Username = "john1"
                        },
                        new
                        {
                            Id = 2L,
                            Avatar = "avatar2.jpg",
                            Biography = "Biography2..",
                            Email = "two@mail.com",
                            Password = "$2a$11$iwYqm0R35nj0BxxxTwze.uIE8uo05vw5h48MgogXtws1LltRYzEx6",
                            Username = "paul1"
                        },
                        new
                        {
                            Id = 3L,
                            Avatar = "avatar3.jpg",
                            Biography = "Biography3..",
                            Email = "three@mail.com",
                            Password = "$2a$11$sQ0DCXKl1xPMMWKcuNQroOh1ICzVfDvElSRmMZVWwikl6VB6Vz8Jy",
                            Username = "bob1"
                        });
                });

            modelBuilder.Entity("Domain.Entities.UserRoute", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("RouteId")
                        .HasColumnType("bigint");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoute", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            RouteId = 1L,
                            State = 1,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            RouteId = 2L,
                            State = 2,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            RouteId = 3L,
                            State = 1,
                            UserId = 3L
                        });
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<long>("RolesId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");

                    b.HasData(
                        new
                        {
                            RolesId = 3L,
                            UsersId = 1L
                        },
                        new
                        {
                            RolesId = 2L,
                            UsersId = 2L
                        },
                        new
                        {
                            RolesId = 1L,
                            UsersId = 3L
                        });
                });

            modelBuilder.Entity("RouteTag", b =>
                {
                    b.Property<long>("RoutesId")
                        .HasColumnType("bigint");

                    b.Property<long>("TagsId")
                        .HasColumnType("bigint");

                    b.HasKey("RoutesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("RouteTag");

                    b.HasData(
                        new
                        {
                            RoutesId = 1L,
                            TagsId = 2L
                        },
                        new
                        {
                            RoutesId = 2L,
                            TagsId = 1L
                        },
                        new
                        {
                            RoutesId = 3L,
                            TagsId = 3L
                        });
                });

            modelBuilder.Entity("Domain.Entities.ImageLocation", b =>
                {
                    b.HasOne("Domain.Entities.Location", "Location")
                        .WithMany("Images")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Domain.Entities.ImageMoment", b =>
                {
                    b.HasOne("Domain.Entities.Moment", "Moment")
                        .WithMany("Images")
                        .HasForeignKey("MomentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Moment");
                });

            modelBuilder.Entity("Domain.Entities.Location", b =>
                {
                    b.HasOne("Domain.Entities.Route", "Route")
                        .WithMany("Locations")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Domain.Entities.Moment", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Note", b =>
                {
                    b.HasOne("Domain.Entities.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.HasOne("Domain.Entities.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Route", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Tag", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Entities.UserRoute", b =>
                {
                    b.HasOne("Domain.Entities.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RouteTag", b =>
                {
                    b.HasOne("Domain.Entities.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Location", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Domain.Entities.Moment", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Domain.Entities.Route", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
