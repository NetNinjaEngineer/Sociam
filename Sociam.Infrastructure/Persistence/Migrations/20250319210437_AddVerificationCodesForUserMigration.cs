using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVerificationCodesForUserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceVerificationCode",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeviceVerificationExpiry",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59b652b4-8b40-4132-b18a-046191128fc3", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 36, 138, DateTimeKind.Unspecified).AddTicks(1744), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAENd/F+uen6X4NFN/hQg+I9CVGW77jgwScn2sOgWAz9iqzkVAEiJhYxUNt2ZwtOQF8w==", "c90c53a5-2a7c-4ef5-9839-7f41a0142fae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad77f20b-7a34-450c-b672-bb1d66946d88", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 563, DateTimeKind.Unspecified).AddTicks(8071), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEJmIDpj70JvWWyQm9JnmcBb+agZw3sUrUVw4Qv4O5UGHP1/JNJ8hn9RiYXaegIAveQ==", "e65e3c77-26cb-4dec-96eb-fa7b650fc3fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f6d8df8-061e-4977-bc09-17013f726113", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 36, 41, DateTimeKind.Unspecified).AddTicks(5632), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEEVIFH4bDV6h+M4OFcbXgAWqA1qD4Y9+jzdo/AV4Y37yAsK1AFjNUl0ZG2vikxWVzQ==", "92b7fda0-06a4-4e52-9f7d-ab01facf80c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b2f7a64-4c66-45c3-8261-cdb2afe8466b", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 950, DateTimeKind.Unspecified).AddTicks(8566), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEF2T4A9ladQNvn5xFaJSFDsyOOoV3ETHjsE5YTl16ml6jWff1bqM5yjtx90GqPPSUA==", "645d7e72-d716-4b23-b107-c2a6dc897549" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8305631-f823-4571-a00c-3b7a40de30d1", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 256, DateTimeKind.Unspecified).AddTicks(7187), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEJDLLzNQxIRq+CwgmnxnSspqDbWJ97L85zy1aPfWVuX6di1huuaEOMoLN2Dn42lWzw==", "b9de0f17-ee6d-4b2f-b965-e084dfda8c02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b734c7e5-6ada-4633-842c-6d06127ddc20", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 753, DateTimeKind.Unspecified).AddTicks(42), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAECfGY3YVr3o/asYW/UpXkL8nWq7/0gzAjTM2lFDW3CTMzvKzakNQPpp6i6Yq+ZZgdA==", "b92996b2-4749-4b05-864c-219341b8c0d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8a98e44-8a75-4fcf-a710-2ec122378f6f", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 658, DateTimeKind.Unspecified).AddTicks(6921), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEJP0oABBWskga4Osp/xns8tVdqlmJZKUKhsC74Duct1OuGyb9nX5qbgdT7jWaCQq9g==", "c09999e5-aa50-46aa-9845-9a0ad2667024" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "410b4a35-7a82-47d6-8802-ce1b2fd3c60f", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 154, DateTimeKind.Unspecified).AddTicks(6336), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEKQuw+KjCoQwcviwAEsSyvxNCT03hmxhY3yVYZnE/QFWj8h1Xn9HgwpW7ldn1pZBFQ==", "d55c97c5-b8f2-4263-8353-da16f45d63e5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99d4bd9e-746d-4e07-b45a-32c61096416a", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 351, DateTimeKind.Unspecified).AddTicks(2201), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEAtMslJxjIiRr+laweQo8a/wvaFZQ6Mzoftis/vtAIpsAsr3ckE/qRGiXNh1JWxo+g==", "d755037d-0457-46b1-87a8-f06baef3331b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5d3b9f6-582f-4ec2-a905-cd2d128143a0", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 853, DateTimeKind.Unspecified).AddTicks(4445), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAEEIiU8DFZswzRVCWzBnCGLZ/zeWtFUJdLPjcUzTr36vla5CVQVh3UgNdkz9A/Yk1Rw==", "f5eedfa5-3a8b-4b5c-b32a-e76bcd562483" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DeviceVerificationCode", "DeviceVerificationExpiry", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fbaff2e-4a3d-4086-9bc1-7fcf931c193c", new DateTimeOffset(new DateTime(2025, 3, 19, 23, 4, 35, 448, DateTimeKind.Unspecified).AddTicks(9400), new TimeSpan(0, 2, 0, 0, 0)), null, null, "AQAAAAIAAYagAAAAELwKcfbRcG0WqE7I9Nro9IujInlLWzLQWn5kUXRXFgj/8sYpizP8so9CN1JEpQoD6Q==", "9b7c01ec-7ae9-4c0a-8775-c345fc8f328d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceVerificationCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeviceVerificationExpiry",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45fda0a0-042d-4e6a-ba7d-a9ea9f1696a9", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 51, 580, DateTimeKind.Unspecified).AddTicks(7736), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEFUZ//nxMID7o+1tmqGUrpm4TxYm6Eo/uSyPxDCCS3kzx6dVkm5uMKGfQJAtIvtujg==", "1b92a5cc-56cc-472b-abeb-93bd0ca52aba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54bd5dde-2cb1-48b9-b91e-3ca6012e2313", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 50, 990, DateTimeKind.Unspecified).AddTicks(3185), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHlMCWK+kp0UfhPfpkSvXiNLBPZ8LrxJ7LT/hPNq+a9aFY2RKg+Q2ajuQ/si7rSlTQ==", "f2bb69a3-b1e2-4be9-8a3f-d17137ab897a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "663dfd71-7880-4b1d-92b8-b87e4767c77e", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 51, 485, DateTimeKind.Unspecified).AddTicks(3103), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEMQfITisudHmtNf31BpKHlib1P8yMIo4il5Ab/603k+sWcgAJF0wy8wVDKFrPPPxxA==", "cd6506f3-c4ce-4315-a22a-994eb669ee36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0f99a0d-7ff2-42f0-8f24-a71291651baa", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 51, 393, DateTimeKind.Unspecified).AddTicks(1905), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAECLgcQDUDr1YvZ1bbDWjepNVgB/bvpQ24khAukVTHebH8X2Cl+TXHDp/KjpnwT3PNA==", "9c3304ca-d220-40c1-b40f-da3a82776d58" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06050a80-7bfb-42eb-87dd-8397b955d74d", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 50, 702, DateTimeKind.Unspecified).AddTicks(9206), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEPEpNFIswwuc1Vhmw05rW9YD7vZi56mJlhheMX9ad5Wv8l1fMZE2LeARvE5h0lA4xA==", "1e2d8fbb-7c3c-44bf-9e48-2827cd2e501f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "310ad630-c414-4a05-8045-99a2aa86cc39", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 51, 203, DateTimeKind.Unspecified).AddTicks(8034), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEGt+nQrs7xghLmuXRvZXVH5nHv814wf+t1Zm6MQvAt/+CeMNUPrqsn63a3KlbKmo/w==", "a4745bea-6bc4-46e0-b282-e3d86c99d122" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9eac5b7b-cd0c-4258-9451-fb5020d466c6", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 51, 108, DateTimeKind.Unspecified).AddTicks(1396), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEARS30lSw0KydiQ9QRwGO4Yqikn7VZPoz0pKr77JQ9+pdo5onx3cBptlVZa+/sPZlg==", "3df398be-f92f-4436-8847-41d63defd7f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c7438fa-2317-4b65-8fb6-2f4b470bcaad", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 50, 606, DateTimeKind.Unspecified).AddTicks(7930), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEFrkkfZ+ISXuQvRf6Eo4ZfGnQM+KepGYpt7lUKUthzyCuUde+xjCFYjuMrYHKx3E+A==", "b04c3924-7523-43c6-9b36-a3af406ec28b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee991594-17dc-4b17-a876-fe06d44ebeac", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 50, 800, DateTimeKind.Unspecified).AddTicks(7754), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHSO8MZfpW0Qw1E1VCT9qU7mk3+cPsbp6SVudcYcFiak/ZamcUYDMRVueVYCiMurIg==", "682d049a-40b4-4df6-86b1-74c6e477b105" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b973157f-9a4d-4107-9be9-c6b5fc1745f1", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 51, 297, DateTimeKind.Unspecified).AddTicks(2437), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEIu+GdiP9hFgMPzOgUwCSn6u9Anvm5NWapNp75bHRIsLVV67FT2KYKz+cYB7Y+498g==", "5e5e062c-d281-4d4e-9920-c8a3d68bc849" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9d6e3c0-9569-4aa1-a29f-5fad6725153f", new DateTimeOffset(new DateTime(2025, 3, 19, 22, 53, 50, 898, DateTimeKind.Unspecified).AddTicks(3583), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEHh9L42WUmpG0QGPwCO8uMhv5iB+403n1DkaxLQMvn0SL1dzlrdSjsWw6DrJq3RklA==", "f98e6942-820e-4ba1-8556-2080482d3c10" });
        }
    }
}
