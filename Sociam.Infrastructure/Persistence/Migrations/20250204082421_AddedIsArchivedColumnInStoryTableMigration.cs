using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsArchivedColumnInStoryTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Stories",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Stories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53dc931c-f08c-4fe7-a280-92503bd27a55", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 54, 120, DateTimeKind.Unspecified).AddTicks(9221), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKgOCQg9Cp8PtrRAgcQH4VHUww4ldlcwlcTbRAoZJL00BWq5/XcaK1G/Ciu8w8u5Pw==", "2e302437-fda0-4927-8b80-e24fd25d06ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4cd7e7e0-6923-4eba-b5cd-d1e7e36dcbb5", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 541, DateTimeKind.Unspecified).AddTicks(4698), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEJ2xcFD0y9uaOUdg1w2vqDB0MyHR/ecvs8a7AfonPn4PXDUWNW89I4qazMAA46xSQ==", "c0e4391e-8039-4914-bef8-925faa7edef4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "968a08cc-d3ce-402c-94d9-62e0bbe5e7f9", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 54, 28, DateTimeKind.Unspecified).AddTicks(6682), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEGktEu5LLabSWtkhvwqgzrTSslkPqcKxupWvr/v7VV7UyQF+egf6srW8VdQYicxWnw==", "d5a19fe9-c559-4009-917e-eb1a4a4b5a97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4940f5ff-5139-4888-a2cc-cae6105afd29", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 935, DateTimeKind.Unspecified).AddTicks(7828), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEPD6oZ53o89mVqmRZMmKnvlw7qCoFyc4OGIxbmufkMoBDmhucUKnXsC/8GTMRPPVqQ==", "86b683a7-2b08-45f6-8b39-094078501b89" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6f19b7f-e01f-4d5e-8fef-2cfb9706477f", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 260, DateTimeKind.Unspecified).AddTicks(4306), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOYdsGDVRdde2mZ3LyIhMnwdjXEiTXhX8FxEzFV9UdZaMhwBD0FExJCcLSQratEG7g==", "9ccc3fbb-936d-4423-884e-ceffb9afdb4c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7adff7e3-f6de-46c9-8793-7d3a5a889bf2", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 738, DateTimeKind.Unspecified).AddTicks(1062), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHJh3JIUifpan82M1WGG9VIzr4MWMNxUQA602ivAv3ehOywgWc9RdX6nlDdssZvr5A==", "1162f4f8-9fc0-482f-87d2-6a3569b159a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3203e52a-a40a-4b3c-9901-c20b097df17b", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 633, DateTimeKind.Unspecified).AddTicks(9721), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEJXmycowuJCVGXYVo9NqFEMTt1iPMGplWv97q3FQLe07xdwg05pHE96wWvbqG9FCQA==", "51a48068-14ac-4912-8674-263028ad15b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f534aafc-9741-4334-a139-3434e567c7b4", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 146, DateTimeKind.Unspecified).AddTicks(1133), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHD9TRMieDJHQ5pihfHG1cUjpqF8rphit8+yBZJc8iTpyt2nHBKEfyc++NtMl2FZCA==", "60e58b8b-189f-4a5c-a9e5-a76915510397" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cdf3de5-79c8-42f5-90f6-0a32779db2c1", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 355, DateTimeKind.Unspecified).AddTicks(7621), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAED/Ul1n/F+Ip0ShNrsyfz33utBCKUBW0uXeAzeUny3PQRmHyMXbOSjMZ7lIhl1uu/A==", "4ff1d34f-1441-48b8-bd3a-ff89ed8b3f05" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4059761e-f3b3-418c-b9f3-24ff2a7b221e", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 841, DateTimeKind.Unspecified).AddTicks(9455), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAECDw/0ECh3+1ntnIgNcphAPHtyq1grFEaz3MGcPvDzTxW8yrQ/r8zgg/Qyrev9KLvw==", "474816be-c608-4e33-92cf-6332e2230cdd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a2bd6ee-a57f-453e-9339-744ba564d720", new DateTimeOffset(new DateTime(2025, 2, 2, 14, 41, 53, 449, DateTimeKind.Unspecified).AddTicks(150), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEOw8FR8bHztK48YPVbuaRLMDlZFFufdW6y2EuLHS37/y1EsjW5dF7AWYGBruF8ex8A==", "45d0de0b-974e-4e15-a168-d968afd1d1ec" });
        }
    }
}
