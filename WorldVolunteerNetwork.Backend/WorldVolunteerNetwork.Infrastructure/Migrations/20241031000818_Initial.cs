using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorldVolunteerNetwork.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organizers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    years_volunteering_experience = table.Column<int>(type: "integer", nullable: false),
                    acts_behalf_charitable_organization = table.Column<bool>(type: "boolean", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: true),
                    social_medias = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_name = table.Column<string>(type: "text", nullable: false),
                    permissions = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteer_application",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    years_volunteering_experience = table.Column<int>(type: "integer", nullable: false),
                    experience_description = table.Column<string>(type: "text", nullable: false),
                    is_member_of_organization = table.Column<bool>(type: "boolean", nullable: false),
                    name_of_organization = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer_application", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organizer_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    organizer_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizer_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizer_photos_organizers_organizer_id",
                        column: x => x.organizer_id,
                        principalTable: "organizers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    organizer_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "fk_posts_organizers_organizer_id",
                        column: x => x.organizer_id,
                        principalTable: "organizers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_photos_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vaccinations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    applied = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vaccinations", x => x.id);
                    table.ForeignKey(
                        name: "fk_vaccinations_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "posts",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "permissions", "role_name" },
                values: new object[,]
                {
                    { new Guid("abefff7f-c4af-40a4-8a57-e540a85f71a3"), new[] { "posts.create", "posts.read", "posts.update", "posts.delete", "organizers.read" }, "ORGANIZER" },
                    { new Guid("b8c09528-1648-4921-b493-a66669854796"), new[] { "volunteer.applications.read", "volunteer.applications.update", "organizers.create", "organizers.read", "organizers.delete", "posts.read", "posts.delete" }, "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_organizer_photos_organizer_id",
                table: "organizer_photos",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_photos_post_id",
                table: "post_photos",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_organizer_id",
                table: "posts",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_role_name",
                table: "roles",
                column: "role_name");

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_vaccinations_post_id",
                table: "vaccinations",
                column: "post_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organizer_photos");

            migrationBuilder.DropTable(
                name: "post_photos");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vaccinations");

            migrationBuilder.DropTable(
                name: "volunteer_application");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "organizers");
        }
    }
}
