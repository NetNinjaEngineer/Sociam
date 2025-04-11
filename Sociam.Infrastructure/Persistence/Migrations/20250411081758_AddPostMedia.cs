using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPostMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    MediaType = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostMedia_Posts_PostId",
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
                values: new object[] { "1b66b206-8064-407d-bdd2-2cf09521fe77", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 57, 161, DateTimeKind.Unspecified).AddTicks(7772), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEB3nY8Dd6LI3iZIKP0PRoboDam+SJmZlgtWfk14tqQYmfd7C1MjDJYsOxwDKPZ8xpQ==", "92c76722-2103-45b5-9902-a880110c9944" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44ae05e5-6c6e-46a8-b7d3-88b696612dcd", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 597, DateTimeKind.Unspecified).AddTicks(4236), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEBESuSL0+claX4AAeX2g43k4qgXSkP2pFDGZVxHozt9e1F3NDgVwYLmAP0qCgle3gg==", "ae97571d-e04d-46bb-9929-a0f9b9fbc982" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69dfed65-ac25-4d7c-8d98-6717e70a272c", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 57, 70, DateTimeKind.Unspecified).AddTicks(6100), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEFi3l/Bstq6P3FpOvZnQyI2ojhHNBIMRDFS+ZuVOeo++ZLRnYexomu1RCVjyp4OkwQ==", "18692596-afe1-4e8a-882a-f0d7b63a6d84" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8369705-334b-4543-8328-31be651394c2", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 978, DateTimeKind.Unspecified).AddTicks(9230), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEPhbSfmHGxTv5HKcheUBH5BcRZ/yu/7+DNkmL+A3XZHuXosb2WcRyMIPp+5VAKQdtA==", "7a5de383-14a8-4034-ab6b-1d186827de4c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d32065f-f1bf-4699-94f0-9f8b8e63a07f", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 322, DateTimeKind.Unspecified).AddTicks(8816), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEICulYmVbs+du8qugJvhEqjWTIFcar5JH/jMkTvRg47j8c8coIcBF2vmM6wLju/LfA==", "8945d039-fede-4d34-8d8b-903e7640ae11" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0587308a-b90e-482d-89de-8ce307f99507", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 785, DateTimeKind.Unspecified).AddTicks(858), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEENIdgV10NntOa+b1TMf+8QNnRnl/flyP9wEDUGVcVp4racg+LsrYzQPazQgnUu8eQ==", "b1bed1c3-d87b-4144-8d9c-594fa695f6d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d929b39-e655-45d5-b682-756f5aeff06d", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 690, DateTimeKind.Unspecified).AddTicks(1041), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHQM8mV5Xw4oMfkelq7SKxJttP0g2QzBdRhICksLnV4FmviasF5TG9neh37MSO3+5w==", "f3c5136a-1677-4f38-8c7a-b8efd1758a09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b35e969-7ac2-4299-8556-6dd80f4a10a6", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 132, DateTimeKind.Unspecified).AddTicks(6373), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEGVvTGjcwonYKw3/Q+5Dz2kUGPVb82a/2EOt402nFBwneAg1k0auSXZMD7+uP1jxMA==", "8e7bffa8-6848-4cd5-9efc-97e97d07467c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83DFC31A-11E5-4AD3-955D-10766FCAA955",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97650e8e-bdd5-437f-a82a-2eec2a0df4e9", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 230, DateTimeKind.Unspecified).AddTicks(8137), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEF2C7KgGLsjju3qiMzz4RACCikWvNAzyj3dW9QxIJzC3ArtDBvJssrcWdol9efAoLA==", "2c75e8ce-fa4d-4966-bda6-983d7771e69c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33579811-0b4d-43d3-bc8a-4672f51f556c", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 414, DateTimeKind.Unspecified).AddTicks(6355), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAAO60E7q56Jgg8gpvPqv1QTZBGhJygXEhQOwEXFwEPbeihjkFgHW+rwOeIK51eTuA==", "16bf1746-22b7-4db1-adde-2f8a631366cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31a4da75-4b99-40d6-9020-5fcc367500cf", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 884, DateTimeKind.Unspecified).AddTicks(4470), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOjEpVpHd2H/6GQIZ4FpyJXcoNXWqnHH7zUmcsssUF1pYb7iL2o5smnSFQTuB3+qvw==", "eaaf6845-3743-442d-8092-79c7f3e38ef7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f31717c8-f61d-4c64-95d7-db4d8fd4e989", new DateTimeOffset(new DateTime(2025, 4, 11, 10, 17, 56, 506, DateTimeKind.Unspecified).AddTicks(4231), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOVcqNRnY5eQOGBYX6/wAwJVIoiqghBw/6hmdpXZ+T1G8b6axn4du2Dl85m2/+6LIQ==", "bfaa4a8e-89df-4086-8616-c2a9f5a8d42d" });

            migrationBuilder.CreateIndex(
                name: "IX_PostMedia_PostId",
                table: "PostMedia",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostMedia");

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
        }
    }
}
