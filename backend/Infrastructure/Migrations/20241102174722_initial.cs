using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    Biography = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Avatar = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, defaultValue: "/user-default.png")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Moment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Coordinates = table.Column<Point>(type: "geography (point)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageMoment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    MomentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageMoment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageMoment_Moment_MomentId",
                        column: x => x.MomentId,
                        principalTable: "Moment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Coordinates = table.Column<Point>(type: "geography (point)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RouteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RouteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RouteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteTag",
                columns: table => new
                {
                    RoutesId = table.Column<long>(type: "bigint", nullable: false),
                    TagsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteTag", x => new { x.RoutesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_RouteTag_Route_RoutesId",
                        column: x => x.RoutesId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoute",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    State = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RouteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoute_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoute_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageLocation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Availability description", "Availability" },
                    { 2L, "Interests description", "Interests" },
                    { 3L, "Road description", "Road" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "Biography", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1L, "avatar1.jpg", "Biography1..", "one@mail.com", "ddfsdfwfe", "john1" },
                    { 2L, "avatar2.jpg", "Biography2..", "two@mail.com", "ddfsdfwfe", "paul1" },
                    { 3L, "avatar3.jpg", "Biography3..", "three@mail.com", "dfghjkj", "bob1" }
                });

            migrationBuilder.InsertData(
                table: "Moment",
                columns: new[] { "Id", "Coordinates", "CreatedAt", "Description", "Status", "UserId" },
                values: new object[,]
                {
                    { 1L, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (48.858779767208894 2.294590215790281)"), new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6833), "Moment description 1", 4, 1L },
                    { 2L, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (55.751165864532894 37.61726058361952)"), new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6836), "Moment description 2", 5, 2L },
                    { 3L, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (50.04278538093594 87.40137428089643)"), new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6839), "Moment description 3", 3, 3L }
                });

            migrationBuilder.InsertData(
                table: "Route",
                columns: new[] { "Id", "Description", "Duration", "Name", "Status", "UserId" },
                values: new object[,]
                {
                    { 1L, "Route description 1", 1, "Moscow - Saint Petersburg", 1, 1L },
                    { 2L, "Route description 2", 10, "Mountain tour", 1, 2L },
                    { 3L, "Route description 3", 20, "European travel", 0, 3L }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1L, 1L, "Tourist" },
                    { 2L, 3L, "Bad road" },
                    { 3L, 2L, "Diving" }
                });

            migrationBuilder.InsertData(
                table: "ImageMoment",
                columns: new[] { "Id", "Image", "MomentId" },
                values: new object[,]
                {
                    { 1L, "Image1.jpg", 1L },
                    { 2L, "Image2.jpg", 2L },
                    { 3L, "Image3.jpg", 3L }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "Coordinates", "Description", "Name", "Order", "RouteId" },
                values: new object[,]
                {
                    { 1L, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (55.877806802128674 37.560213608843085)"), "Location description 1", "Moscow", 1, 1L },
                    { 2L, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (50.62478102741532 86.32222662409758)"), "Location description 2", "Altai", 1, 2L },
                    { 3L, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=0;POINT (48.8615359552553 2.358705469822234)"), "Location description 2", "Paris", 1, 3L }
                });

            migrationBuilder.InsertData(
                table: "Note",
                columns: new[] { "Id", "CreatedAt", "RouteId", "Text" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6761), 1L, "Text 1" },
                    { 2L, new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6763), 2L, "Text 2" },
                    { 3L, new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6764), 3L, "Text 3" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "CreatedAt", "Rate", "RouteId", "Text", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6794), 5, 3L, "Text 1", 1L },
                    { 2L, new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6796), 4, 1L, "Text 2", 2L },
                    { 3L, new DateTime(2024, 11, 2, 17, 47, 21, 115, DateTimeKind.Utc).AddTicks(6797), 3, 2L, "Text 3", 3L }
                });

            migrationBuilder.InsertData(
                table: "RouteTag",
                columns: new[] { "RoutesId", "TagsId" },
                values: new object[,]
                {
                    { 1L, 2L },
                    { 2L, 1L },
                    { 3L, 3L }
                });

            migrationBuilder.InsertData(
                table: "UserRoute",
                columns: new[] { "Id", "RouteId", "State", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, 1, 1L },
                    { 2L, 2L, 2, 2L },
                    { 3L, 3L, 1, 3L }
                });

            migrationBuilder.InsertData(
                table: "ImageLocation",
                columns: new[] { "Id", "Image", "LocationId" },
                values: new object[,]
                {
                    { 1L, "Image1.jpg", 1L },
                    { 2L, "Image2.jpg", 2L },
                    { 3L, "Image3.jpg", 3L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageLocation_LocationId",
                table: "ImageLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageMoment_MomentId",
                table: "ImageMoment",
                column: "MomentId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_RouteId",
                table: "Location",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Moment_UserId",
                table: "Moment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_RouteId",
                table: "Note",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_RouteId",
                table: "Review",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_UserId",
                table: "Route",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteTag_TagsId",
                table: "RouteTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CategoryId",
                table: "Tag",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoute_RouteId",
                table: "UserRoute",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoute_UserId",
                table: "UserRoute",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageLocation");

            migrationBuilder.DropTable(
                name: "ImageMoment");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "RouteTag");

            migrationBuilder.DropTable(
                name: "UserRoute");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Moment");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
