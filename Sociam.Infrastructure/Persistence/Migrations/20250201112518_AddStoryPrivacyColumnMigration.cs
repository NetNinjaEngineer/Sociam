using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStoryPrivacyColumnMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoryPrivacy",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f141d2c9-8da1-4484-93d9-eef89ebc7122", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 16, 637, DateTimeKind.Unspecified).AddTicks(8394), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEFNo0zpt8QwjCn4hLZz4TW2jhgZMuwQ5CjKB2bLjJKgy4RLJn6l7jihh/FIvKWbicg==", "06cf952e-1f32-4484-b4de-1c0708347f16" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6053763c-e15d-4ee3-ae46-f858f39fe7ac", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 16, 48, DateTimeKind.Unspecified).AddTicks(6134), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEGEvVynl4i9RI0wKW/WYyoYbIegBDVp7cNQW9LxC6uYWfj3GlvFqyliBOjkvarN+ZQ==", "a785164a-c0ae-4100-80b9-9d1d0bf62bce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6ee6ff5-a9cf-4ea7-b6d4-0d378390545c", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 16, 546, DateTimeKind.Unspecified).AddTicks(4028), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEPQ4t9sOPNI7Qy5IMcIZtei/okWHy7KQ7YQSQ06I6jdzonWkiVSHRjgcVtG3TpYEFQ==", "98c78dc0-2ab1-4acf-9ff9-4a72a71d664b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf79dbd5-3eda-4970-b35a-710c9c2872dc", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 16, 443, DateTimeKind.Unspecified).AddTicks(7075), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEI1K40h6hsXI57WFFARALbTh7t9k3I5Y4A7aACf4cJIkfvreG8cYw0G6DOJTqJ9gFQ==", "d37d198e-e1aa-418b-82f4-243d17a00cfa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a4fc0ba-e539-4c32-ba92-6b2d4332d8ea", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 15, 766, DateTimeKind.Unspecified).AddTicks(949), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEC8JX72DKUj4lflzmfs4DhLeSxQCu+SusOaWuJkyBeJUn3/JkxIK++JJsI7b1MqAQA==", "49f9c8a9-f8a4-40f7-8161-440cb753f11e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54d9ceeb-af0a-4980-b15a-41990d448766", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 16, 239, DateTimeKind.Unspecified).AddTicks(648), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEK5pYiR8nTosIhrgF90xNKN84F+HoBT5BIiAEVF1lP+uXIyjQMyzlOfR8r/XwKyKBg==", "d7f2a1a2-1a07-4ebe-9b0d-a620a7af2b65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d204cb8-c98a-4a29-9d6f-f4b6e6f674e6", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 16, 143, DateTimeKind.Unspecified).AddTicks(9156), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEJiOk6h+sywEA6vobLI9RkoNQ998sd/VUr3L25oAW2lq3hKyQHqEjeA0R8EnPjrKBg==", "bdaa6b77-6b56-4b92-8499-c635804efa0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d33c1b6-6c56-4cec-aed8-0cc0a9938b39", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 15, 668, DateTimeKind.Unspecified).AddTicks(3343), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKiVgBWVVHK9k9p1Qo7Ot6PGiySh5koP+dTeo7dQ/c5Y9jD6oBocM1rKGM3rylpa3Q==", "5c6904d8-d3d1-4e2c-9945-4c2c4ae06859" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f659314-0f8c-4aab-9ecb-336d862d50d4", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 15, 859, DateTimeKind.Unspecified).AddTicks(6848), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEONSWdCbqoMCxxORR01akXgJEu44ZEQnii3l4sRDpItwX2nvZJhFs17koTm5DFCMfA==", "091074a6-d340-4e94-9ad0-09420a35f938" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "648307a2-1007-4a61-b176-7ca6eb91bfd9", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 16, 337, DateTimeKind.Unspecified).AddTicks(9543), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAECzYtfTaq+IZboK0xcHQR3LcDiUJPsmZ4fSauCXmxXyToqg+1oK93hfK7ebwK0ConQ==", "9e118115-92e6-4465-8389-bbd0c8839742" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bc9ca2c-7454-4ead-b3e7-ad93f2cdea9f", new DateTimeOffset(new DateTime(2025, 2, 1, 13, 25, 15, 953, DateTimeKind.Unspecified).AddTicks(8603), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEO+LIltvARplxYYHup9Fq+lMVUixS46zpRNx2WhlFbRS23AVeV+NlAb9r0TNjaa4dQ==", "c26ea357-5a0f-424c-9ca6-13b81c372ec6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoryPrivacy",
                table: "Stories");

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
        }
    }
}
