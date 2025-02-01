using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatedStoryCommentAndStoryReactionTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoryComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CommentedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryComment_AspNetUsers_CommentedById",
                        column: x => x.CommentedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryComment_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryReaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReactedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReactedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReactionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryReaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryReaction_AspNetUsers_ReactedById",
                        column: x => x.ReactedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryReaction_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "875aaf74-56b7-4b74-839a-35968320a2a2", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 45, 142, DateTimeKind.Unspecified).AddTicks(6468), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEBr156cZL07C/VojsbaIjZs2te4kUR6DQsDejxMpXAO98BH5z20HlwX/hHWkLkPZ3A==", "522b48c1-6050-478b-85ec-b26477c9537e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd17b931-be29-46fe-8162-0c6bf80412cd", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 573, DateTimeKind.Unspecified).AddTicks(4677), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEPnufPj1zPA0cZJ05axEM/PY45ZEUjnbGaR/PnnOdhGA1297pK7OalLy568PcLnH5A==", "599cbed0-94cb-4f55-986f-36f73d79e7b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a2c7188-8622-4e71-b4a9-7b0a65607e2f", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 45, 50, DateTimeKind.Unspecified).AddTicks(9741), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEIqt+aOtMbEMirEiWEfLguYPV+Q59k6XmrCwKgPHDaKyOxOnJz2wW+lrwB1BulFesA==", "fe36e30e-2a50-4831-872c-eef4ac1b8353" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c2e311e-360d-41fa-b55e-1877df517570", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 958, DateTimeKind.Unspecified).AddTicks(1176), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAENQEX9rHIOOGXD8kPizktrOdekOl1ARI7kEKLOyNbd14s5/lxeBa8yXyyIdTLnKDZA==", "1dd188fe-390a-4d1f-a23f-a1a3fe12fb03" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d560526-32d2-42f2-9001-2505876acff0", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 253, DateTimeKind.Unspecified).AddTicks(9865), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEN+AuWDmbuqI6IMjcTWfnH+gqioSgkHY28H6mhgieAK/JmAWTbVbMZ5Nn+k/1SNvmg==", "9651c051-4ba7-47e5-8d2b-aeeda22718ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9b68cef-cf26-48bf-9378-07eefb762f46", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 763, DateTimeKind.Unspecified).AddTicks(5317), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHgBBrCVoHq1hf8mXuJb5d/chtXc2hD4Wn3xoh3qYFaRhXqXvAfnXNOfjyW276zDZg==", "60e3edbb-3abe-4168-9245-c4d66e3f1504" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ede5c291-7965-4bb3-8f99-f42073fed215", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 666, DateTimeKind.Unspecified).AddTicks(1310), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEN2o5/jpNIragp/4jkaF+N70vC1Fqm2Nun3Te+3nEwiuQe5wSyJLU53HdnICRMGxuw==", "d88c70ce-2968-49a5-b3c1-c33faf32adf0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1c47b0c-95d8-470d-94fe-9fc47e30dd05", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 158, DateTimeKind.Unspecified).AddTicks(3962), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDfXOV45qBmv0BcnoderiptTTAYPKB9lO3N7GgynAIVWfuM2pimg1UhtqfnUq/nbdg==", "2f4cabcf-37a8-49a5-98b7-c7d4aebeb768" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54066e93-0f2b-4024-b7c5-cae37a15ae1e", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 354, DateTimeKind.Unspecified).AddTicks(6994), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHcLrAQjKrWds5EEpjgO6whlTgiq7v6ARxEDibTrXWDtu6FUMEpgbsb+V4j0cVjdhQ==", "0a44f9c4-ba89-4acc-b525-26fc2814a066" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8c91af2-d3bb-4b92-9919-56dbc9b7b210", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 856, DateTimeKind.Unspecified).AddTicks(1704), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEgnYQhVLfgH3WnehZv+Hqb1I871Tw2F9Xp4RQOeaP51DNeDQZeUUmgOf6GgTlcASQ==", "7b1b8ed9-9cd5-452d-9308-a300d8e93301" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ce156ee-82e0-48c1-8f2e-9cedfa8bcf2f", new DateTimeOffset(new DateTime(2025, 2, 1, 12, 21, 44, 458, DateTimeKind.Unspecified).AddTicks(759), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAED05rLsD6o1d95zfxjnOJDkcBKORKKMHw0Na7YT5+nhI8IWha9ByplVYTagC9JBSSA==", "d8d63a5f-8520-45d0-b5e7-22ed8ca581e3" });

            migrationBuilder.CreateIndex(
                name: "IX_StoryComment_CommentedById",
                table: "StoryComment",
                column: "CommentedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoryComment_StoryId",
                table: "StoryComment",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryReaction_ReactedById",
                table: "StoryReaction",
                column: "ReactedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoryReaction_StoryId",
                table: "StoryReaction",
                column: "StoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryComment");

            migrationBuilder.DropTable(
                name: "StoryReaction");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc3e9033-c2de-4207-9ffc-e0cf9858e43b", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 22, 427, DateTimeKind.Unspecified).AddTicks(3600), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEORyLaz06rvHXsHbS3x0N2HawKU5tBOwWIsUW4+AUQvJV86VuMzDsSKGdH681mnWcg==", "d5f3a210-0316-4f8f-92f5-f053cafd457f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99b22141-f7ca-46bf-8da4-ff28d77c42e2", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 21, 719, DateTimeKind.Unspecified).AddTicks(9111), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEH7j4DPBv10RNlTW5mZBoLhxd2QCPkeHWs3v7UOip9TzEfwuyRVVpW9YyBS/dYkdoQ==", "a23402d9-39d1-42da-a364-393cb7f52ae5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ea1b65d-d786-4562-a8b0-1c7775536574", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 22, 310, DateTimeKind.Unspecified).AddTicks(9461), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEP+uMo0HTytH2CYWQoGAkq6Evz5f0olq0NsRD7aw+aknO4Z76JP4vbIO5e4loyW6Jw==", "03f07a54-0719-4aa7-8e60-1e5499870332" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1665c6f-1b25-44eb-8106-3e9305398b3b", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 22, 197, DateTimeKind.Unspecified).AddTicks(794), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMeGyqtxgrUNcg92niQvT3nGseE1MjFCc5b6rxD32aLUPNh1cIcDloREbXAyMEdU0g==", "d3d313b6-7ae3-436f-9060-478882132696" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e446375-73b9-4fdc-9da9-792673248c75", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 21, 393, DateTimeKind.Unspecified).AddTicks(4494), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEGCiemLwGfxXAc4CoznnvRZiBm83oDkn1mMMTpD4S7abxErV/uJOBSjiyHbsvqe+fQ==", "5f7904b5-0d75-4561-a97a-13f9cc6733f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58a569f0-2652-48a4-847b-412127ea7d8f", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 21, 956, DateTimeKind.Unspecified).AddTicks(2103), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEJEi700FStmx9XBIMHe95R1yhTzgci01r5/dJB0mm+iauHLy82U9kyd5WbSy0YLhmg==", "38d5ce88-8821-4473-880b-1646d40b572c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0451d098-f81c-4f0e-9591-7768a8d8a274", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 21, 818, DateTimeKind.Unspecified).AddTicks(4549), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAELBZ7lprHoqz7Pv7QDLosrnPAkcXU4j56AGSwpXAEFXGoAJmtnr/jQdLSEmSobw/3w==", "2d2e59f7-b014-4c0e-b23d-20eeed82937d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "294a4228-e18e-4f1a-9123-edef73d4e457", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 21, 292, DateTimeKind.Unspecified).AddTicks(6905), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOQjnVlTazSmcdRiDii8NohmZ4kg2DcwkWaL/HH1IRGgBDp4XAdfbZxO0XTYd//e1Q==", "ac67d65d-80a1-4fdb-87e6-2b0dffa6803e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ec342ca-f724-45b6-8b92-758d90afcdbc", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 21, 519, DateTimeKind.Unspecified).AddTicks(656), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAENN+6kynbP10e3N4otlkr4VFaBOQUn6glxmNfiNYXygQNGURGsQIGo6pVCw9EHZQPg==", "3a9981e6-ae59-4fef-852c-c0e80a088835" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bcb412d-34e7-4c02-bed9-7825f02287a7", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 22, 85, DateTimeKind.Unspecified).AddTicks(1427), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMLGVm2rhaqgjge+S6o8Xi2rKtf4QUeGuwP9zUUKEd7D1Z5P70CqhEDQISJno0avBQ==", "ad0be384-8354-43ca-9331-d729862fd8fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "145a25a9-0696-4487-ad7b-e9a2268f21e0", new DateTimeOffset(new DateTime(2025, 2, 1, 10, 29, 21, 621, DateTimeKind.Unspecified).AddTicks(4166), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEBb4tG0zI3iY17zNv6OVPRot3f9gGSvQgEvQLVjw/hWCll9oHjxRLWYmuHRalrj+4Q==", "0bbe63eb-9def-4c4b-848b-7adbf2b02be3" });
        }
    }
}
