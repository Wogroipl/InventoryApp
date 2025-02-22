using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryBlazorHybrid.Migrations
{
    /// <inheritdoc />
    public partial class AddHallandContacttoJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VenueHallId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ContactId",
                table: "Jobs",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_VenueHallId",
                table: "Jobs",
                column: "VenueHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Contacts_ContactId",
                table: "Jobs",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_VenueHalls_VenueHallId",
                table: "Jobs",
                column: "VenueHallId",
                principalTable: "VenueHalls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Contacts_ContactId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_VenueHalls_VenueHallId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ContactId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_VenueHallId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "VenueHallId",
                table: "Jobs");
        }
    }
}
