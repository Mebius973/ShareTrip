using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripParticipant_Trips_TripId",
                table: "TripParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripParticipant",
                table: "TripParticipant");

            migrationBuilder.RenameTable(
                name: "TripParticipant",
                newName: "TripParticipants");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Pictures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Commentaries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripParticipants",
                table: "TripParticipants",
                columns: new[] { "TripId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TripParticipants_Trips_TripId",
                table: "TripParticipants",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripParticipants_Trips_TripId",
                table: "TripParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripParticipants",
                table: "TripParticipants");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Commentaries");

            migrationBuilder.RenameTable(
                name: "TripParticipants",
                newName: "TripParticipant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripParticipant",
                table: "TripParticipant",
                columns: new[] { "TripId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TripParticipant_Trips_TripId",
                table: "TripParticipant",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
