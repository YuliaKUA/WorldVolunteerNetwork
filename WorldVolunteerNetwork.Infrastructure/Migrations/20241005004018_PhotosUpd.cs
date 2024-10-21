using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldVolunteerNetwork.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotosUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_organizers_organizer_id",
                table: "posts");

            migrationBuilder.AlterColumn<Guid>(
                name: "organizer_id",
                table: "posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_posts_organizers_organizer_id",
                table: "posts",
                column: "organizer_id",
                principalTable: "organizers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_organizers_organizer_id",
                table: "posts");

            migrationBuilder.AlterColumn<Guid>(
                name: "organizer_id",
                table: "posts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_posts_organizers_organizer_id",
                table: "posts",
                column: "organizer_id",
                principalTable: "organizers",
                principalColumn: "id");
        }
    }
}
