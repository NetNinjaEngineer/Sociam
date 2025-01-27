using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateJoinGroupRequestTableWithFluentConfigurationsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupPrivacy",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "JoinGroupRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RequestorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinGroupRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinGroupRequests_AspNetUsers_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinGroupRequests_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinGroupRequests_GroupId",
                table: "JoinGroupRequests",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinGroupRequests_RequestorId",
                table: "JoinGroupRequests",
                column: "RequestorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinGroupRequests");

            migrationBuilder.DropColumn(
                name: "GroupPrivacy",
                table: "Groups");
        }
    }
}
