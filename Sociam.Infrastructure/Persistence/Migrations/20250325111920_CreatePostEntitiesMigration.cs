using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatePostEntitiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    SharesCount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Privacy = table.Column<string>(type: "text", nullable: false),
                    OriginalPostId = table.Column<Guid>(type: "uuid", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<string>(type: "text", nullable: true),
                    Longitude = table.Column<string>(type: "text", nullable: true),
                    Feeling = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_OriginalPostId",
                        column: x => x.OriginalPostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostComments_PostComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "PostComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostComments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReactedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ReactedById = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostReactions_AspNetUsers_ReactedById",
                        column: x => x.ReactedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaggedUserId = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTags_AspNetUsers_TaggedUserId",
                        column: x => x.TaggedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "239ac919-bb2a-4193-82de-4317d8f488ab", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 846, DateTimeKind.Unspecified).AddTicks(8609), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAELh3GJo+0dxUldbKM7NyDJg5YJOAf+2Nix1hyUSP1KvSPA3ASPhyub8aSU7kyTaJNg==", "7742ee08-65f9-495d-ab79-81f24ee4fd2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4691629e-38b5-4245-a981-838e495666f1", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 271, DateTimeKind.Unspecified).AddTicks(5321), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMAYseGYpeFsdfc/j/N1zFSWVS3gxf8rAQbkHjTKMkMrxFjzm9IBGU00noQjbvTM9g==", "3c810e9a-640a-4887-9a3d-b87fd92402c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cd09bf9-4c59-4221-8ef1-06e4ffaaaec5", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 755, DateTimeKind.Unspecified).AddTicks(4043), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMRXBMuq7KRjozw1MvMxhh222EXOdFU7TxkdyoxQzAgElvs1aEZ6eJMR+EHibZTGkg==", "d570eadd-c69c-4724-8e74-ba1e9559ad6f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe15b6a4-8f8b-4232-b74d-bd442c9b86bc", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 662, DateTimeKind.Unspecified).AddTicks(5102), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEA5h7oubZr8xqPZbxI+yIb12UWHgpo5MPNTRG9OIK5ZKx8kcn/RYnG+zwIJT1BVQ4g==", "0221f59c-f16b-4a24-8cb6-89ebe27fc6f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53ef7ac2-2b6c-4458-9a51-59f7fb74d1a9", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 17, 980, DateTimeKind.Unspecified).AddTicks(3669), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEP4UXUU9QFIvsXBovTDkamLWcwEITrymRDWfsGGtnDUBtOu12jSWwTL++xyX/z3nMQ==", "78c2e355-624c-4ed4-a532-15d7cb3d13be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5503c0f-6fbf-41de-8283-5b23fce03ecc", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 465, DateTimeKind.Unspecified).AddTicks(59), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEPhxc+XPZurac8xNF1bOmMFXkJH3JJfVij6pFo2iLzqm0JqzRbfC0QBs73rsTY6iFg==", "267e1891-d484-4380-8179-87edce3136d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aad1ce57-8ec7-4d06-a7f2-073f76e87d63", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 363, DateTimeKind.Unspecified).AddTicks(8668), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHdx0lCFOZ+3h8GEKyPS+EJybPcs9ddYjrEfsrOLxduW1S5EUUkbqCzwQiJh5qbAFw==", "b555413a-f189-4f0e-9cc2-accbeed60786" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce2b9380-3825-4074-a9d6-03b42f9130a1", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 17, 781, DateTimeKind.Unspecified).AddTicks(1700), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEnP0ejTVWcz0AKqAWrVNpJmJ5v/0u3/4DWUcah1vifA63w87PRR/+PxL8kaHYQAmA==", "6f0bd96a-0d39-4596-8113-df747c245322" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83DFC31A-11E5-4AD3-955D-10766FCAA955",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e370736-1a7e-4749-8d9b-751a30a32f8c", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 17, 876, DateTimeKind.Unspecified).AddTicks(3802), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEFVbD+RAKQM5OCGJQmK132xAzg1ePWpNnat5jcvxFxHgr/WMgBII6V7ItxDFPs2gbA==", "7d736bc5-3a75-48c4-b25c-7e15f6bd548b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e02cb1de-f72f-4f1d-82f1-5dec04685fe9", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 84, DateTimeKind.Unspecified).AddTicks(1436), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEJ+jjqozeQdi7VPWtgjz/q5oRz54LHJ6bazFCQJTmHOx69TXgHF441cJzFUuNYvlTA==", "3a6a0539-df80-4130-93a9-219ba7786703" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c717f182-5ed9-48d9-bae5-db238207ace9", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 570, DateTimeKind.Unspecified).AddTicks(448), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEGdCdrN1Uzz7N1a/0E9Ev7/nTpShw9uMDgCSTaqWfN/IL5vi+Qi8yx6k2xKkfAqr1A==", "6a622ae5-9668-4442-8cf0-75828e9bd6ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5f9d217-608d-4660-ada9-766d7a5f85bd", new DateTimeOffset(new DateTime(2025, 3, 25, 13, 19, 18, 177, DateTimeKind.Unspecified).AddTicks(9895), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOlr0PTWHcNIyqTOSjYP7TY008wf2hSucPhj+6lEn9tEmNYhsqn0eapq2COgSEfEyA==", "8e7db84d-2c54-46aa-b9f2-67abdcdd63ce" });

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_CreatedById",
                table: "PostComments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_ParentCommentId",
                table: "PostComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_PostId",
                table: "PostReactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_ReactedById",
                table: "PostReactions",
                column: "ReactedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_OriginalPostId",
                table: "Posts",
                column: "OriginalPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TaggedUserId",
                table: "PostTags",
                column: "TaggedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "PostReactions");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd88ff74-8046-4f01-901e-de2d80ad8a34", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 31, 213, DateTimeKind.Unspecified).AddTicks(2484), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEIcd+089w/fuGeAWY9fqa7lzeGPD8+v+ZhCwt0dB8NgxJv5Zqez9PHC9BId476U7QQ==", "19a519c4-2bd0-4160-b931-188e3e3e133a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbcbe875-f5c1-4f5f-80c2-1e9cf81ea8e8", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 595, DateTimeKind.Unspecified).AddTicks(6832), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAENfl8YQykN+830va5iu4JVGAwwCkrciAPpddDLbvUV8pWhJViharzwsqurdcNtn1ag==", "e4890b09-fa47-48c6-9223-c7df005940be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ab9b96b-432a-4d0f-b847-f3dc527cda74", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 31, 106, DateTimeKind.Unspecified).AddTicks(7138), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHfFDNn4EhYD72mcsaEhxn1E2nEK0o5IehBC4w1R8uXw8v/1VHMRSwRXWchLWPm0HQ==", "c697edf7-82e7-4842-8ac3-937c97f62e10" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfff31da-97a9-498c-b30c-5584c3663bb4", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 986, DateTimeKind.Unspecified).AddTicks(1710), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAECX1fgv7CA0dhFqSBM1sbPPWLYucajbbNdtuKaT5eL+BkTcojPCugdbAvVHc6sDKMQ==", "87cf4f3c-8698-4a08-9659-90669b29a961" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2e3186c-2817-483a-8174-d5cf85da126b", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 260, DateTimeKind.Unspecified).AddTicks(1713), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEK7cvJccbSqnU0X60dm4VCm+IpdlpHDKrgLm7Zw5yRNPKpiFBB8WeXcnrEmikvkJKQ==", "bbd70624-ecff-4cf7-b764-6bf20ccb71c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b11206aa-a5aa-477f-a90d-be6ed402a1bb", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 794, DateTimeKind.Unspecified).AddTicks(5769), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKJEVvM0YxuLQn8nR4/2/jUSpRzHvDZ2PUh+KQhFT66K8N4z/0m+dvHWM5bj4ih/fg==", "a59d81ad-689b-46cd-b140-7a133d8b1007" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78e35e66-20c8-4842-baa1-3473285d86b4", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 697, DateTimeKind.Unspecified).AddTicks(1223), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDTSGnpN+ooB3MICjm4FcbkLHZ7iioFdrOXlaD6/kkG1E6Jn3+IkLXvDn3WR0nmo+Q==", "986d8022-3d14-479a-bfa4-2e75dcb54359" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8fc0a6d-cbcc-4f5c-b92c-2700078baa6d", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 9, DateTimeKind.Unspecified).AddTicks(1126), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKffantNQn85DD/c2zS3VENdY+OWCU8MP4flvaZvowhCPcmLX8gwHHxYWC1jdadRrg==", "c6504981-4dee-4c81-9239-9d58fdae278c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83DFC31A-11E5-4AD3-955D-10766FCAA955",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50ba2acc-6337-4ca4-ab3a-353bba67d34e", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 131, DateTimeKind.Unspecified).AddTicks(1907), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEPr4TUK5opIJ5+8Hn+hiYn6k/DJjalSbCDQwCOf/FQKxicuzZblcsOFO9zqcwffM+g==", "63e3cceb-5c24-4921-98d5-5a47a2cbf5cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0580e93f-9a73-4108-beaf-058c043a6a6e", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 386, DateTimeKind.Unspecified).AddTicks(3600), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEIjaUp0KA+4t7WaW/jSUSsWapwNhzesy0/2BYwTtC4s2at5VRTMTV1X2+p90JfreEA==", "a327ceb4-a715-4f4d-9414-1e071d185850" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c57ec4d3-8f1c-4ead-8e9d-8b5c9c0201fc", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 889, DateTimeKind.Unspecified).AddTicks(9156), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOw1Nhvn2daXYKWWqBdpLyQlSVIsQFXEatEgGXNA5VXqIm09Se5km4thOLWKnP6NAg==", "74742616-2325-457d-a9f6-87372ff3ec03" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7d170d6-f485-4f3c-b66e-3ce77424a06e", new DateTimeOffset(new DateTime(2025, 3, 20, 13, 49, 30, 491, DateTimeKind.Unspecified).AddTicks(6848), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDG7XUjd8czTYgYG44rfIlSc0NZE9d+BwrD50B+kTQ3HFDxjQ1j0V76n+xfLqPoLfg==", "35c2e683-d4b5-4a65-af83-a53ee733638a" });
        }
    }
}
