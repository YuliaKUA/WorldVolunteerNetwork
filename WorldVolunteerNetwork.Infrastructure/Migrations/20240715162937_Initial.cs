using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldVolunteerNetwork.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<string>(type: "text", nullable: false),
                    employment = table.Column<string>(type: "text", nullable: false),
                    restriction = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    payment = table.Column<float>(type: "real", nullable: false),
                    reward = table.Column<float>(type: "real", nullable: false),
                    submission_deadline = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_create = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    contact_number = table.Column<string>(type: "text", nullable: false),
                    building = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    postalcode = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_photos_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vaccination",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    applied = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vaccination", x => x.id);
                    table.ForeignKey(
                        name: "fk_vaccination_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_photos_post_id",
                table: "photos",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_vaccination_post_id",
                table: "vaccination",
                column: "post_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropTable(
                name: "vaccination");

            migrationBuilder.DropTable(
                name: "posts");
        }
    }
}
