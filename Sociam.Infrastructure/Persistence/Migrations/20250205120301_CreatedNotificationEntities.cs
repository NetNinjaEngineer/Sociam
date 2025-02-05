using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatedNotificationEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupRole = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaNotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NetworkNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NetworkNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoryNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoryNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0904e25e-6625-4118-98aa-e947c8ddd445", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 3, 0, 163, DateTimeKind.Unspecified).AddTicks(2566), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEI2Ra4bGprQRPeDp0v4nlvVMnC2aRbpzKp6hxjZuGSm0XouLey56cwORwmt/DI0W3Q==", "d3393a33-f174-4c37-930f-7b1df32f5a2a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b1d1304-9c2b-457c-a1b1-424060b020d8", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 554, DateTimeKind.Unspecified).AddTicks(2482), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEBbXXhe3ZV0JHNVwRyvpdM//jXytHDllDkbehcDN3xBT6mpbreiDMOSB8a9BflTzKQ==", "de7f6d9c-5618-47c9-b35e-531b812e32bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f904964-e013-4395-9247-8c64822f3556", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 3, 0, 62, DateTimeKind.Unspecified).AddTicks(4805), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEI31fGGcCGIn7v2eqsjZZBJNo5tHxXk7rFgC1A3E9Rr9dTa+xqV4YbOuJ1r4PBAI7A==", "db8c3489-8f04-4363-be0a-34d635e21fe5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "839cbaef-57fe-42f8-ab90-a9f4f9c500ab", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 956, DateTimeKind.Unspecified).AddTicks(9461), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEwDLPwvPcUd+3kqTIcTG31nMzVCNtMVa8ULKhk6wecQUvkTBy6fDcisU7bwgl/gKA==", "d51e0bf1-01fd-4394-bcba-a24cefece818" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16a1baed-cdeb-4195-b995-8399e9bd32ca", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 266, DateTimeKind.Unspecified).AddTicks(9548), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMp/ZQ2ubcxwGbWGWLVRIyGSiItCD1cbmuDtlMRg7yFozma/8AcoyB8L5uGI0NWKqw==", "a635d71c-cdd5-48d6-ba27-6b53115d9dc3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7030700-9b20-48d6-9914-57585569cfa9", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 761, DateTimeKind.Unspecified).AddTicks(4509), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDZNxGssJh5usjFVrqMUHnIInc9tPxee06ik1KQQV4DJEa15Jpo+s59hFoL24nTx3A==", "a4bdd3e4-7be2-4d2f-9bb6-25d70e071fce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc704a15-b08a-48a3-a5a8-8dc449f3bde4", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 663, DateTimeKind.Unspecified).AddTicks(2424), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEApU1sfp1aEQga1hOhEzlbavC4sXCL/XYK06rABdPRSr0ZQu0ck62KQeonzrrO2pMQ==", "f4178159-fc01-4e93-8acf-c7a92c41b880" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37d10d45-45e7-461e-9fa6-8bd5a8a99910", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 138, DateTimeKind.Unspecified).AddTicks(9804), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMgS+UbcwDta0UECF1zQxt8/96FdCy5zKVQf5A32iXN3lNDeR1xP+Tq7EzD4HT19NA==", "3710bfe4-fe16-488b-90c5-1d9921f3a73f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d753654c-b9f6-4c20-9a3b-b933129b35ff", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 364, DateTimeKind.Unspecified).AddTicks(2960), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEN/i+v+Ti60yWkAr7yxOJzymNht6Utz1U5sNbrbkr52XgSseozIcAcismD8ZETlM1Q==", "472e2329-446d-484e-a871-d0a1cad2b5a4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38ebc180-45c9-48ed-8342-20024d4d4e42", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 860, DateTimeKind.Unspecified).AddTicks(993), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAjYuZGsXf3eDV25j11q2rD/zjrAoDZtSPTzxvpmEqgdDzXE9A19Rv6KzBu9XaNjRg==", "203da17f-4b7b-4321-ba53-1a50df123ad0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81decad2-130b-4be4-bf15-3bc3c6c8ac31", new DateTimeOffset(new DateTime(2025, 2, 5, 14, 2, 59, 460, DateTimeKind.Unspecified).AddTicks(1051), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEG8gucbSFnV3dTyZsn82baC3sUWmoK7z2Xlnx9zSvPu1G2g/+/yj3e5F8niax611aQ==", "f01af506-a8da-4414-a879-71832156fd99" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotifications_ActorId",
                table: "GroupNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotifications_GroupId",
                table: "GroupNotifications",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotifications_RecipientId",
                table: "GroupNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaNotifications_ActorId",
                table: "MediaNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaNotifications_MediaId",
                table: "MediaNotifications",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaNotifications_RecipientId",
                table: "MediaNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkNotifications_ActorId",
                table: "NetworkNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkNotifications_RecipientId",
                table: "NetworkNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_ActorId",
                table: "PostNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_RecipientId",
                table: "PostNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryNotifications_ActorId",
                table: "StoryNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryNotifications_RecipientId",
                table: "StoryNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryNotifications_StoryId",
                table: "StoryNotifications",
                column: "StoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupNotifications");

            migrationBuilder.DropTable(
                name: "MediaNotifications");

            migrationBuilder.DropTable(
                name: "NetworkNotifications");

            migrationBuilder.DropTable(
                name: "PostNotifications");

            migrationBuilder.DropTable(
                name: "StoryNotifications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73694c44-469d-4a48-9e32-cea8c661cb61", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 20, 641, DateTimeKind.Unspecified).AddTicks(9758), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMtMW5l1N/FV/5BZntNjKXFsJJyyyLa5pyNdMlAjax0guUj+3XVbx5aq17Oekg+79A==", "027d42cf-f29e-4306-a89f-7877df1e18b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa183d0c-10d4-4441-9c1f-fa6f7cf80659", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 20, 53, DateTimeKind.Unspecified).AddTicks(551), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMOkYgyL/VzuBD8gPa3KTSDK2Xt/PHdER1tJJfTbaKzQiVBRs3yP9yQWQPl+F9Yu5g==", "4364f810-15c5-4a33-83d9-1476299d69a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12292086-78b3-4490-888c-12a6a4bc5b43", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 20, 546, DateTimeKind.Unspecified).AddTicks(6121), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHKMC8dOHgL8ZeRFmilGYz314vmKDilV6ER+zdFbqyjuGEYK7n2vWgtAeBBsvcM9ng==", "7f7f229b-2807-4888-b95d-902235242861" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c75b3b5-9918-47e9-93dc-3975b2c3f0b0", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 20, 454, DateTimeKind.Unspecified).AddTicks(6973), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEP0gOV9mWLLqwxejiLWY+Z6o2/dqtpU2eMplfGqB5B2OqFpQfKfjMlAmp4u2U8o0tw==", "07721105-84d5-4a88-a6cd-c3478486e67f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2075b21e-4309-4093-bf64-4e29341e30fd", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 19, 761, DateTimeKind.Unspecified).AddTicks(304), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEIS0s17P9m3CYLPvQI3YZKFZQqc7sj1sVUC50MhMjI3VF683ucKRyIvI+yIVDd46RA==", "91e69a0c-4eaf-4c97-bd28-a0b8761f7f8a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "114204d8-0a8c-442e-a5ee-7ca3f820ff59", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 20, 238, DateTimeKind.Unspecified).AddTicks(6926), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDHfjnuAYOwxvT2hZl3S+QU6ZAyCyQcIyXM9q5Et5/dttwczjujpCA+R8ZAclzU51g==", "51706553-da3f-4df8-b4ef-79cf7df70cd6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9622e09f-46ca-4da7-bbd9-50df7e630e2a", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 20, 146, DateTimeKind.Unspecified).AddTicks(7703), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEBMFvRuSE7FKrhd2HAAaPvavsFI633iAdZGITZpcFLCk5BsG1peGMVFeOSBW77a/+g==", "90f9d3d0-902d-4edb-a37c-331e1b6d1fef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65346b9c-ea28-4331-a7d5-fe44e738bdd1", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 19, 619, DateTimeKind.Unspecified).AddTicks(9082), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEhwNff7FLzw47dZfrwaOPADAYteHVqP4aPcg4eI62SNfjo6UVndefk/Eu1KWV02gA==", "8c266431-42f2-443b-bd08-cd7c33403451" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0077d82-9fcd-4dad-82ff-15672168a68a", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 19, 858, DateTimeKind.Unspecified).AddTicks(7210), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOhZ8gW78XxdHFnQSzIFQ7sR8BJj5IxhCL0bX52PTipDt/tcQuHNH9F4y13luuS5hQ==", "ba4a6fb2-328f-4f45-843e-aeaf9a213f73" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52daef92-5b3b-4aa3-9981-3ab8bac4184d", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 20, 348, DateTimeKind.Unspecified).AddTicks(1827), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAL3gKBnI5AZEjUPzbqkK5uxZVuQ593doxcnz+m/2oDJ2XDl600N2GYYEwrE19McmA==", "459eb4d7-f5c0-464f-a334-dbe453b012b2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d55e55f8-50f9-442f-ac7f-ee61f881be43", new DateTimeOffset(new DateTime(2025, 2, 4, 10, 24, 19, 957, DateTimeKind.Unspecified).AddTicks(8826), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAWR8MFMkP/kIShnxcxbsZxt/XzrgWuIX1Ds1U4bKA+xkRsj4AbTK66+bchTeicv3w==", "fe3dc196-bcb5-4d22-bdc0-25e7a9c45676" });
        }
    }
}
