using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedAssetIdAndPublicIdToPostMediaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetId",
                table: "PostMedia",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "PostMedia",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0262d249-044c-492a-86bd-ad23499c010f", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 12, 585, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMM/V9pUdLj3zXk1KbOJ3tCfcAbSgb/7qfzCx94Fl3G57CymzkiyyqmSSzm802ze6g==", "5d36d3f3-1ea9-435e-a679-003bf6c79631" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d59abb28-4578-43ab-b64b-a76dfb6d20c7", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 11, 979, DateTimeKind.Unspecified).AddTicks(9143), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAVvuyihWBsfjtJ+Oa+X1LR9sEmBTqonLrZT8S+T794HGpix0IVPeA3uxJSDp77P0w==", "634ecd94-e992-4f9e-a315-e9af4007a044" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc0292c6-e58c-4db7-af66-6d0376ce6feb", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 12, 476, DateTimeKind.Unspecified).AddTicks(9720), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEeDCve8zGwd6f+KKdNRDPhC7uOlN4IILY5FbruAdazdtsyQtp3ansMvFtevyxuUvQ==", "9ce138c9-8979-45b1-9d04-ee42164f626b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6858a8bf-cb5e-4d77-ba7c-c2893435337b", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 12, 371, DateTimeKind.Unspecified).AddTicks(6338), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEIzvW5NfxiAQlqIro6jZ8bL7gpKxU+fvm26RcxfoIgxh54gdjYtf364EJ7Dy2fyV3A==", "119a79fe-20c1-4078-b3be-5127fce9b646" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "205e0bc6-f73d-41de-876a-1df6669df066", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 11, 684, DateTimeKind.Unspecified).AddTicks(2315), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEE0qyWamshS57MNZF2hdLVi83OWf0fpQFIa3V4xoXhaOyKhW5p6CejTWsAmagKRcqw==", "e00e2d7f-77d1-4ba7-9c90-656e3d355aa4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19070a19-f07c-4b86-8480-c5c770b8b4e1", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 12, 185, DateTimeKind.Unspecified).AddTicks(8569), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAELCFJCRaKKCSq1rVHQ99g76jJ9oMKF9gkTnZZaZuxULuMuPqrTul2iSNK27Rysd5Qg==", "309aae62-f5d2-4be1-9008-a462fe28b1f1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ced2144-5d1d-42ae-91a1-3f2c3d0c04cf", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 12, 89, DateTimeKind.Unspecified).AddTicks(3435), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEJn5Xo+R7oGFOoap0FzsKwt0GT49PqeXr/2mAJUpIKjLE5bojnOcwkpxx5GM1zIrOQ==", "9a763326-77f1-46cb-9400-28129c63de4e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "075061c5-dd85-473f-add5-9f691892ed1b", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 11, 487, DateTimeKind.Unspecified).AddTicks(7402), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEItEkkUE6KGiKLQAxNYU5yTa3tc5PTjfbfDnTuAih0feSKPqjJDvo81jkYfWrpNFLg==", "e775a676-7aa9-4320-9165-382cc6adeb87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83DFC31A-11E5-4AD3-955D-10766FCAA955",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c54c998b-33b1-4b03-8d14-cb862bfb08f3", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 11, 583, DateTimeKind.Unspecified).AddTicks(690), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOyINEU2Pvrd1Umxvz+McbPWpYJkRLhPZ7WNDXSb13ToyiXmuVBds9YKewCES9VHow==", "f17d5f3a-7641-4cd1-8ad8-4acc3fc782ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8fe6612-5920-434f-bdd9-2ec50859859f", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 11, 789, DateTimeKind.Unspecified).AddTicks(2922), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOA0Go67852SDWkFMMOJB01zxsDTGBcUqe6xuW6pZKxGdjjadl4VWVYUW4m6ZfRoag==", "e01132dd-449f-4cec-8e42-82df4e7e25fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89584b3d-5f02-4c05-93aa-3744dc0cb87b", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 12, 278, DateTimeKind.Unspecified).AddTicks(6837), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKSoV9wdGGLlBn2xCPrIxO5cOTtULUAxkprD1YmnBhFvLSa5b2Gb/2ZMorEKaoUV+A==", "7f42b38c-bade-4e07-b24a-c642419f7dd5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c834c9e5-dfcb-4fe2-81db-a3ecc0a12fbc", new DateTimeOffset(new DateTime(2025, 4, 12, 12, 12, 11, 883, DateTimeKind.Unspecified).AddTicks(9437), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEO/XovSop49hHe7fX55xv/ejMfgHg/zWOLgWR30/8BFSLLVZul6wAqKey/NT0w30TA==", "6743080f-6945-4ffc-937f-1c3565cfca82" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "PostMedia");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "PostMedia");

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
        }
    }
}
