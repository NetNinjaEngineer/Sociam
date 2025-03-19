using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIpLocationToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastKnownIp",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastKnownLocation",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02668ec0-082d-4b3b-a4af-4649869e2c70", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 34, 216, DateTimeKind.Unspecified).AddTicks(131), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEHHgDgQd7qQrPlBCW04ZM/Tdp5luU6IRojOq6EzCEuivcYrBTRiaoBq8sk59ZyGY/g==", "894918a3-9461-41f4-8d34-fded3b97180e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9eda8bd1-9d51-4886-a09f-a8b6f15c60b4", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 33, 655, DateTimeKind.Unspecified).AddTicks(2517), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEAXb1nagZCF2MUKZevtQ0yp8yZzMv/6fxC8b2/7NHW9lgTSZd1b+SB5dYB2Ssj8kig==", "1aeda489-3e25-4991-964d-6a5d4fc3f260" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8692f97f-23dc-4c93-a184-adb47b4805c5", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 34, 121, DateTimeKind.Unspecified).AddTicks(9515), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEGMZ26zAZLsQSgdTRXGHGCN284cCBNW/YSLo8jh4V8OTzcMAZBWZxtsOEmLIB9YSuQ==", "66fddd72-7f18-4853-b885-ef9aeec9a82d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e34d66de-e21c-4871-b413-9257d1d7ae12", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 34, 30, DateTimeKind.Unspecified).AddTicks(5372), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEH3W57545m0H1nVxmVyiiWmet+ZoApFlk9tj8Y4vwpkY3L4qYECpWNi9YdRKPKPgYQ==", "55e6a15f-a417-4be8-9d3f-4b45f49a0125" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88640d68-a605-47c2-9bcb-72c432ee8cba", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 33, 352, DateTimeKind.Unspecified).AddTicks(5734), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEB0TuQBXgHIteti+TRNJwR9i+RI9OLeTgTdE+5R9femLOIFq8o/VcF4VBFNDYC010Q==", "17710d11-f05a-4d5b-9465-aba56d605d42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b95cef8-5449-40a5-9042-9556af8cc63a", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 33, 847, DateTimeKind.Unspecified).AddTicks(2705), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAENvWX0cRCLpmIE014M/cwUSg8u0JHZjP1nL5bIgs73UcxPBBDYvpk3EX94sDpU7QaQ==", "eccc78db-25a8-44d6-b057-c5cb9df2ed3d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b456d73-40fc-4b95-8ffc-ee819f9357dc", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 33, 749, DateTimeKind.Unspecified).AddTicks(8268), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEP1qlDJ+lpTToPDlRgNsn/VituykCbQBNSj53LsPS/1tu+zFG3oRgTR5VYYgVz+WMw==", "1ccfcd34-4957-4155-b611-a32320248bdf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3c32058-e521-422a-80d4-39ea3d3cdb47", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 33, 256, DateTimeKind.Unspecified).AddTicks(9631), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAED8TuN/P42vqkXaUdi4Ku4lCnChw9sEItzxbY14EH1bz0/PesIjuWHKEBVQjhilpJw==", "6077ca62-f7eb-461a-aa49-b47a5e717caf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "844401a4-413b-42df-ab11-4c4d5bb569d1", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 33, 451, DateTimeKind.Unspecified).AddTicks(8680), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEGfa1F3TEUfFnm4b0kB3as5ajkxqDhhiAE9fgMJH1VA80opcwr+pDsoOr+IZ+TpAfQ==", "72c0ad7c-a191-46b9-b148-35b9f1209fe5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9e54776-3b92-4ca6-a81f-9576592e0619", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 33, 939, DateTimeKind.Unspecified).AddTicks(7945), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEIj1ePJZhiy3QZi6OfkYY5hRYbxAEORM3Px/Yve5ZKrnd/TxSOIlcvvefmCWAx4jGw==", "87b7dbc4-0251-407c-a7fc-f51ff6e5b76b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastKnownIp", "LastKnownLocation", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63911ac2-70bb-475e-8c0a-973361ceeb92", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 9, 33, 553, DateTimeKind.Unspecified).AddTicks(232), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAECauJNwOjCvKTP3/RZyHl/zqeuFtPbKCL2fSASCswLjTTa1FXsfXfAKN9a/qMFhg3g==", "37459c41-98f5-4cfc-9a07-64dc28055cbf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastKnownIp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastKnownLocation",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59b652b4-8b40-4132-b18a-046191128fc3", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 36, 138, DateTimeKind.Unspecified).AddTicks(1744), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAENd/F+uen6X4NFN/hQg+I9CVGW77jgwScn2sOgWAz9iqzkVAEiJhYxUNt2ZwtOQF8w==", "c90c53a5-2a7c-4ef5-9839-7f41a0142fae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad77f20b-7a34-450c-b672-bb1d66946d88", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 563, DateTimeKind.Unspecified).AddTicks(8071), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEJmIDpj70JvWWyQm9JnmcBb+agZw3sUrUVw4Qv4O5UGHP1/JNJ8hn9RiYXaegIAveQ==", "e65e3c77-26cb-4dec-96eb-fa7b650fc3fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f6d8df8-061e-4977-bc09-17013f726113", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 36, 41, DateTimeKind.Unspecified).AddTicks(5632), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEVIFH4bDV6h+M4OFcbXgAWqA1qD4Y9+jzdo/AV4Y37yAsK1AFjNUl0ZG2vikxWVzQ==", "92b7fda0-06a4-4e52-9f7d-ab01facf80c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b2f7a64-4c66-45c3-8261-cdb2afe8466b", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 950, DateTimeKind.Unspecified).AddTicks(8566), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEF2T4A9ladQNvn5xFaJSFDsyOOoV3ETHjsE5YTl16ml6jWff1bqM5yjtx90GqPPSUA==", "645d7e72-d716-4b23-b107-c2a6dc897549" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8305631-f823-4571-a00c-3b7a40de30d1", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 256, DateTimeKind.Unspecified).AddTicks(7187), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEJDLLzNQxIRq+CwgmnxnSspqDbWJ97L85zy1aPfWVuX6di1huuaEOMoLN2Dn42lWzw==", "b9de0f17-ee6d-4b2f-b965-e084dfda8c02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b734c7e5-6ada-4633-842c-6d06127ddc20", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 753, DateTimeKind.Unspecified).AddTicks(42), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAECfGY3YVr3o/asYW/UpXkL8nWq7/0gzAjTM2lFDW3CTMzvKzakNQPpp6i6Yq+ZZgdA==", "b92996b2-4749-4b05-864c-219341b8c0d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8a98e44-8a75-4fcf-a710-2ec122378f6f", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 658, DateTimeKind.Unspecified).AddTicks(6921), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEJP0oABBWskga4Osp/xns8tVdqlmJZKUKhsC74Duct1OuGyb9nX5qbgdT7jWaCQq9g==", "c09999e5-aa50-46aa-9845-9a0ad2667024" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "410b4a35-7a82-47d6-8802-ce1b2fd3c60f", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 154, DateTimeKind.Unspecified).AddTicks(6336), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKQuw+KjCoQwcviwAEsSyvxNCT03hmxhY3yVYZnE/QFWj8h1Xn9HgwpW7ldn1pZBFQ==", "d55c97c5-b8f2-4263-8353-da16f45d63e5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99d4bd9e-746d-4e07-b45a-32c61096416a", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 351, DateTimeKind.Unspecified).AddTicks(2201), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAtMslJxjIiRr+laweQo8a/wvaFZQ6Mzoftis/vtAIpsAsr3ckE/qRGiXNh1JWxo+g==", "d755037d-0457-46b1-87a8-f06baef3331b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5d3b9f6-582f-4ec2-a905-cd2d128143a0", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 853, DateTimeKind.Unspecified).AddTicks(4445), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEIiU8DFZswzRVCWzBnCGLZ/zeWtFUJdLPjcUzTr36vla5CVQVh3UgNdkz9A/Yk1Rw==", "f5eedfa5-3a8b-4b5c-b32a-e76bcd562483" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fbaff2e-4a3d-4086-9bc1-7fcf931c193c", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 448, DateTimeKind.Unspecified).AddTicks(9400), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAELwKcfbRcG0WqE7I9Nro9IujInlLWzLQWn5kUXRXFgj/8sYpizP8so9CN1JEpQoD6Q==", "9b7c01ec-7ae9-4c0a-8775-c345fc8f328d" });
        }
    }
}
