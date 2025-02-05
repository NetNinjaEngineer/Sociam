using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMessageFieldToNotificationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "StoryNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "PostNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "NetworkNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "MediaNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "GroupNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a33bc34d-32b1-4d6f-bd52-7f07370ec6aa", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 33, 17, DateTimeKind.Unspecified).AddTicks(2982), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAENHCH80WwmyOqS5lLAneeTdXbqIfTR+gXdDusYWQCeI4CPakbjYj6EDXbry8+ajQnA==", "766e4937-aa21-4815-930f-1fbf4016a4bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdd4bb60-9d17-4484-a885-c183f7922f33", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 461, DateTimeKind.Unspecified).AddTicks(1748), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAb5Y1s9V5oxDJX61wp6fmaXUr0WsZKvBVBkO1jHIiQ297Zkj9DVPptYZhLv68cG6Q==", "a4ae9944-afcf-4eca-b96d-a4571bd08035" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ae23480-1c57-4ffd-805c-37a86f21dd08", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 923, DateTimeKind.Unspecified).AddTicks(3535), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEGLBSWkd/i88j3ifiLxkG/qt942rZdvxIqTGmGkTKTXRcdFe3AN2OwlC6BlsV1CgmQ==", "35fc41d5-5d97-4dc7-8963-6d8bc68a3c88" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08e9503e-1933-4824-983e-612e6a67e4a7", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 831, DateTimeKind.Unspecified).AddTicks(5456), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDa87ZzxKxpl8O/LT2/OREEO+KFV/STRKwNrYgSv4F9xC+5AWxPiabv/ydBvHmIfCQ==", "bd4cd611-3626-4dab-bc16-f896cd54faaa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "065723d5-5147-485c-86e7-8f81c9e1a591", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 169, DateTimeKind.Unspecified).AddTicks(8400), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDMUnBgcNij1aYyHZpwTCD0OC2pqYnkyn8IPey5gPCEsrErv6lWhrNowtw0R+Km/1w==", "0fc2d8d3-dc8d-4ab2-9407-b12c3b8cf17a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "947a415e-5551-4316-94db-8f2d8feb3118", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 647, DateTimeKind.Unspecified).AddTicks(2695), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEEpffJULy7XZqyr0foKaTdwGxD1P2NnShmoqaubK4HjQKcCTrlLTuJOl3818nW2syw==", "ad4e78c7-4182-4618-9dbc-0e9299b1162d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3de478cd-96a8-4f01-94cb-17832e4c8f82", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 553, DateTimeKind.Unspecified).AddTicks(8369), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEB0LLGg+cNb/DTZSwx48U+K/91Dx87U71Hwuz4xuQ51bj4xXGW24/aI5Axo9zdOozw==", "e5ded411-60cb-4091-b695-ea1f0a04bd92" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5865c37e-46e9-4354-9929-f1e47e3ab272", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 75, DateTimeKind.Unspecified).AddTicks(8939), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEECEJlqAbnv4HrZGIDpg21V3cCSzSCdETzvsXj4TLyMFneVydw5ODtctNRPyhqXvaA==", "e96d9e60-59c4-4014-83cb-6b13adc8c77f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78e0dc20-8f95-4978-bd9c-8b1013742476", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 264, DateTimeKind.Unspecified).AddTicks(6906), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAgxZZyxX1Z+MRLpaT/fa5pMX5nafxnVBlDv62ccW4m4b59nYoYNPxkK8A/NOAtmVw==", "dd8f1a98-d570-4a2c-a83c-192e5562973b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b43d6903-c913-4066-8818-d6b60a0a66ce", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 738, DateTimeKind.Unspecified).AddTicks(7935), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKfiv2zTkiAHadxPQaO+moOvdTkRaWnJ9qkfVl+m/JS2a8XbRU78IIfMWJRoYgNB4g==", "492376f3-97db-478b-bf58-aa4370ca4047" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac015acf-4f49-438f-932e-be1d56de8829", new DateTimeOffset(new DateTime(2025, 2, 5, 15, 34, 32, 363, DateTimeKind.Unspecified).AddTicks(5051), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEAjfsmDmVHfjrqsTtUD1nHiWts1HQ4Bw2l7kIzgI2Xw1xB/xbw34/ZLfhx8+HH5+Hw==", "5266cde6-04e6-4722-bf22-e64d587be123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "StoryNotifications");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "PostNotifications");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "NetworkNotifications");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "MediaNotifications");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "GroupNotifications");

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
        }
    }
}
