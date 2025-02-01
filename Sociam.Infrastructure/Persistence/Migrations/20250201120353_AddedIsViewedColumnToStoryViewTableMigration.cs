using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsViewedColumnToStoryViewTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ViewedAt",
                table: "StoryViews",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<bool>(
                name: "IsViewed",
                table: "StoryViews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20ced5de-adcc-4d82-8397-2e1387390590", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 51, 734, DateTimeKind.Unspecified).AddTicks(1741), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKf/A9MaBdKdv8ewrY0VkQ6h7Sq5z4JCI53PAaEucEpK0hpFVhULf0kJ1wRgMzqpgg==", "80edd01f-28c7-4709-a1b9-29d519977dd8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ee01ee8-ffac-40ee-bfad-39befd815c29", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 51, 155, DateTimeKind.Unspecified).AddTicks(9119), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAED7oBMKXekF3kbFjyCNF1zX/KkdJndbG32R+Lum6eyeaHVky9vdJyD/WNkIt98kPxA==", "e889cc60-85b6-4b0e-9512-2f7ac17c2cc3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92e225f0-5d18-43de-9b6e-5359dccefde9", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 51, 641, DateTimeKind.Unspecified).AddTicks(7692), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDE68YGKqoLYT/GnQEUxUvtS7YHLsQUTD7Ri6hJhfisg5BJwWCXczuU0PPWboOf8ew==", "cb3b5ba6-2137-48c9-bb0b-1dee4435f845" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3897f205-84b9-4c83-99ac-273aaa5e3c01", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 51, 549, DateTimeKind.Unspecified).AddTicks(2929), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEPUVq0OpV5lFjTjzWI/eRELDYMg7g13WcIytyiqwSoOYWAFWOzaIVqjqv8mD9kSFGQ==", "f119d733-63d8-47ae-aabb-dc2eaa4f341f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc1dbaad-2cce-4470-aa04-767377b49a33", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 50, 859, DateTimeKind.Unspecified).AddTicks(2784), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEO8g921i8gsr0cL2OzPbC9IH8KZhJgwFWsE1l3F4nFqWNtRXlYLRxUowlJJZRXOWeA==", "e84241eb-4baf-420a-b386-69f05123eff0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0724393a-1a84-42ca-b7c7-435af424b4ea", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 51, 339, DateTimeKind.Unspecified).AddTicks(7040), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEB0XLt438qKS6RoRdOSVK9oFTe2YpZqGV5UONdt4fwobFqGeMPctu0u3JBY0WdLfHw==", "2776ef0d-144f-4fff-900a-19f07891f15f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b838130-ebfc-47c3-bd80-19fe2b5bad17", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 51, 246, DateTimeKind.Unspecified).AddTicks(5598), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAECQXU8JVtb5tjxhQJH6To3wJMmcZVDhF669z4elAiGbbC3o1FGZZlO36B+g2cu9LWw==", "12920cab-7096-4320-a917-06fd5ce86099" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59853ed2-9034-4a19-857a-78c8391baf53", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 50, 726, DateTimeKind.Unspecified).AddTicks(9076), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEElS2MIyvjs4FBgSsDsRfbMoFqnQ9iqN+RSKGYGzaJeBMb5CSIjMSDrOOPOGZUc8xg==", "6c5196ba-561a-4870-ba60-cfd116215534" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c88ed16-f09c-47c3-9f3e-ba19533b0500", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 50, 973, DateTimeKind.Unspecified).AddTicks(3871), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEE49uUQhnUUAksDX/Ydg4eOCh6KF76nF2qBxcYuA7WNRahYaCr6aVQUBjPomf9PKUQ==", "6e2646b6-81b4-4685-b25a-c73f7dbb6ef0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48587361-29c5-4ce0-a5da-60692d188de7", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 51, 448, DateTimeKind.Unspecified).AddTicks(4949), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEKV1Od1N4yexWad5s297QtA63D/zeFtyCe8aG86jPxEJhKdbEgwAACa+2YGyFoAHSA==", "9d7cd808-3165-4c0e-9509-f44cfab02a1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60c493ac-c116-4c3d-b137-79d4c74c22fe", new DateTimeOffset(new DateTime(2025, 2, 1, 14, 3, 51, 65, DateTimeKind.Unspecified).AddTicks(4870), new TimeSpan(0, 2, 0, 0, 0)), "AQAAAAIAAYagAAAAEDOpadCPE7sf8bTYJWP6JgoymUA9AnEcZSusfBCh5W3fIaeKjRrMUMoTCJchQwR1kg==", "ec0c7f6d-ae86-4ff3-badd-cfd32f56df3d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsViewed",
                table: "StoryViews");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ViewedAt",
                table: "StoryViews",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

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
    }
}
