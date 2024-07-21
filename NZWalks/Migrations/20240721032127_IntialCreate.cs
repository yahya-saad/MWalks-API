using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Walks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LengthInKm = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walks_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Walks_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7a1bc1f1-9dcb-42d1-906d-f5bb7921d001"), "Easy" },
                    { new Guid("7e22f1d8-3c3a-4e74-bb38-5b350156b9fc"), "Expert" },
                    { new Guid("9294e56f-6b1d-4784-b64c-ec86ad3a3f98"), "Hard" },
                    { new Guid("fdc29a29-09c9-4897-8c6d-bd62b2f9eab4"), "Moderate" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("29ff1a54-b7ad-42a0-9e74-fc8cc51bda7b"), "WLG", null, "Wellington" },
                    { new Guid("3f88b2a8-50c4-46e6-8e4f-9a9b9b4f0ae5"), "HAM", null, "Hamilton" },
                    { new Guid("c084a776-99eb-4c2d-9efb-4f964e17f4a3"), "AKL", null, "Auckland" },
                    { new Guid("d1c9914b-05c3-4d7a-86ab-1b3c5827cbbc"), "NAP", null, "Napier" },
                    { new Guid("f3d6b858-d0b3-4012-a1d8-50da8cbba12e"), "CHC", null, "Christchurch" }
                });

            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "Description", "DifficultyId", "ImageUrl", "LengthInKm", "Name", "RegionId" },
                values: new object[,]
                {
                    { new Guid("0a2d547c-7ef0-4bbf-918d-6dfe56b8bc02"), "Kepler Track is a 60 km walk.", new Guid("9294e56f-6b1d-4784-b64c-ec86ad3a3f98"), null, 60.0, "Kepler Track", new Guid("f3d6b858-d0b3-4012-a1d8-50da8cbba12e") },
                    { new Guid("56a9b16f-6cfa-4d25-92c7-63d0e3f6d6e4"), "Milford Track is a 53.5 km walk.", new Guid("9294e56f-6b1d-4784-b64c-ec86ad3a3f98"), null, 53.5, "Milford Track", new Guid("c084a776-99eb-4c2d-9efb-4f964e17f4a3") },
                    { new Guid("7eafc632-1ed5-4317-8ff0-5a4d015e5c98"), "The Tongariro Alpine Crossing is a 19.4 km walk.", new Guid("fdc29a29-09c9-4897-8c6d-bd62b2f9eab4"), null, 19.399999999999999, "Tongariro Alpine Crossing", new Guid("29ff1a54-b7ad-42a0-9e74-fc8cc51bda7b") },
                    { new Guid("82a9b1f6-4e7e-4d85-9f8d-763e3e8b08f5"), "Abel Tasman Coast Track is a 60 km walk.", new Guid("7a1bc1f1-9dcb-42d1-906d-f5bb7921d001"), null, 60.0, "Abel Tasman Coast Track", new Guid("d1c9914b-05c3-4d7a-86ab-1b3c5827cbbc") },
                    { new Guid("ff0bb60f-f7f1-442b-95e8-e2c9f6d97ad6"), "Routeburn Track is a 32 km walk.", new Guid("fdc29a29-09c9-4897-8c6d-bd62b2f9eab4"), null, 32.0, "Routeburn Track", new Guid("3f88b2a8-50c4-46e6-8e4f-9a9b9b4f0ae5") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Walks_DifficultyId",
                table: "Walks",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Walks");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
