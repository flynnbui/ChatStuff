using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatStuff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FriendsCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "Friends",
                table: "AspNetUsers",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "ReceivedFriendRequests",
                table: "AspNetUsers",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "SentFriendRequests",
                table: "AspNetUsers",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friends",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReceivedFriendRequests",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SentFriendRequests",
                table: "AspNetUsers");
        }
    }
}
