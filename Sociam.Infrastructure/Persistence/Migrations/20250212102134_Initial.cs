using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sociam.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeExpiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TimeZoneId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequesterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendshipStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupRole = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    GroupPrivacy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiveStreams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsLive = table.Column<bool>(type: "bit", nullable: false),
                    ViewerCount = table.Column<int>(type: "int", nullable: false),
                    RecordingUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveStreams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveStreams_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaNotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NetworkNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NetworkNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrivateConversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastMessageAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    SenderUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateConversations_AspNetUsers_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrivateConversations_AspNetUsers_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RevokedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    StoryPrivacy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StoryType = table.Column<string>(type: "VARCHAR(5)", maxLength: 5, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashTags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoryNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Privacy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryNotifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoryNotifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFollowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollowers_AspNetUsers_FollowedUserId",
                        column: x => x.FollowedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFollowers_AspNetUsers_FollowerUserId",
                        column: x => x.FollowerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupConversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastMessageAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupConversations_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMembers_AspNetUsers_AddedById",
                        column: x => x.AddedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JoinGroupRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RequestorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinGroupRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinGroupRequests_AspNetUsers_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinGroupRequests_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "StoryViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ViewerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsViewed = table.Column<bool>(type: "bit", nullable: false),
                    ViewedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryViews_AspNetUsers_ViewerId",
                        column: x => x.ViewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoryViews_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrivateConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ReadedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    MessageStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_GroupConversations_GroupConversationId",
                        column: x => x.GroupConversationId,
                        principalTable: "GroupConversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_PrivateConversations_PrivateConversationId",
                        column: x => x.PrivateConversationId,
                        principalTable: "PrivateConversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AttachmentSize = table.Column<long>(type: "bigint", nullable: false),
                    AttachmentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageMentions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MentionedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MentionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageMentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageMentions_AspNetUsers_MentionedUserId",
                        column: x => x.MentionedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageMentions_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageReactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReactionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReactions_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageReplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    EditedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RepliedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReplyStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReplies_AspNetUsers_RepliedById",
                        column: x => x.RepliedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReplies_MessageReplies_ParentReplyId",
                        column: x => x.ParentReplyId,
                        principalTable: "MessageReplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageReplies_Messages_OriginalMessageId",
                        column: x => x.OriginalMessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", null, "User", "USER" },
                    { "BE3B9D48-68F5-42E3-9371-E7964F96A25D", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "Code", "CodeExpiration", "ConcurrencyStamp", "CoverPhotoUrl", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TimeZoneId", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { "049759F5-3AD8-46BF-89EE-AC51F3BEED88", 0, "Gamer and tech enthusiast.", null, null, "84d98d3a-643b-4db1-85fb-04cb7cb8601c", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 33, 445, DateTimeKind.Unspecified).AddTicks(9338), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1985, 2, 14), "chrisa@example.com", true, "Chris", "Male", "Anderson", false, null, "CHRISA@EXAMPLE.COM", "CHRISA@707", "AQAAAAIAAYagAAAAEOcOA+/b9HuoZXj2fTsdT3VoRl9usP/JAKWW6BDamp808tjm3qzBWFu134RS5m6+nw==", null, false, null, "30f59d72-e24e-4264-89ad-06971e059b56", "Egypt Standard Time", false, null, "ChrisA@707" },
                    { "0821819C-64AE-4C73-96F2-4E607AA59D7E", 0, "Tech entrepreneur and mentor.", null, null, "8d98d3e5-ec94-4ac8-8b5b-615ea63e2df4", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 32, 836, DateTimeKind.Unspecified).AddTicks(7239), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1980, 12, 5), "bobbrown@example.com", true, "Bob", "Male", "Brown", false, null, "BOBBROWN@EXAMPLE.COM", "BOBBROWN@101", "AQAAAAIAAYagAAAAEOFN/GQt8slgStaGFTvO0DksqC8Iwvcv5DH045vxnIgfVGmLDD58ZRxQhz+fwEYq0w==", null, false, null, "58831c96-1cb0-4083-8d91-01d57d37f0a0", "Egypt Standard Time", false, null, "BobBrown@101" },
                    { "0A9232F3-BC6D-4610-AAFF-F1032831E847", 0, "Nature lover and environmentalist.", null, null, "d69475f2-93a0-4266-9e9e-830ef2ec9f69", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 33, 336, DateTimeKind.Unspecified).AddTicks(4392), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1990, 6, 20), "laurat@example.com", true, "Laura", "Female", "Taylor", false, null, "LAURAT@EXAMPLE.COM", "LAURAT@606", "AQAAAAIAAYagAAAAEHQ+HKy+yx4lvdOs4bIZsEhj38s74d6aDz8sGohMWRtVpGNuYqN2BGSuy37NcLc2fw==", null, false, null, "5187869f-fd37-44f6-9b16-84f5d0b8954e", "Egypt Standard Time", false, null, "LauraT@606" },
                    { "3944C201-0184-4F97-83A6-B6E4852C961F", 0, "History buff and teacher.", null, null, "baed667c-273b-48a2-90fc-8e304c097dbb", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 33, 227, DateTimeKind.Unspecified).AddTicks(1892), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1975, 11, 12), "davidm@example.com", true, "David", "Male", "Moore", false, null, "DAVIDM@EXAMPLE.COM", "DAVIDM@505", "AQAAAAIAAYagAAAAEBDnwnf72s4H4x6ZosNvwNFvEhbChpQl83ll2TSX5O7ug+T51z0vKD5FZGK98280EA==", null, false, null, "ffe43e9e-0335-4e4d-b5ae-18bd10d725b7", "Egypt Standard Time", false, null, "DavidM@505" },
                    { "3EB45CDA-F2EE-43E7-B9F1-D52562E05929", 0, "Loves hiking and photography.", null, null, "567d24a1-df4a-4f63-9000-7a509c80782b", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 32, 533, DateTimeKind.Unspecified).AddTicks(4019), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1990, 5, 15), "johndoe@example.com", true, "John", "Male", "Doe", false, null, "JOHNDOE@EXAMPLE.COM", "JOHNDOE@123", "AQAAAAIAAYagAAAAEEX3jD84fq9OPy4Uo6sBLapAn44rtfUSI66DqGV33V1GwWVt+8jgIU4aaQ9nVUEHKw==", null, false, null, "651a6aa6-8d39-4eb2-9fe1-2cb24a454e31", "Egypt Standard Time", false, null, "JohnDoe@123" },
                    { "5326BB55-A26F-47FE-ABC4-9DF44F7B0333", 0, "Musician and songwriter.", null, null, "6739dfaf-2981-4ec9-b3da-d2671453e5a1", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 33, 25, DateTimeKind.Unspecified).AddTicks(5819), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1988, 9, 25), "michaelw@example.com", true, "Michael", "Male", "Wilson", false, null, "MICHAELW@EXAMPLE.COM", "MICHAELW@303", "AQAAAAIAAYagAAAAEC1f5ErfiIdIB2/pmM1SH2/wTkWkvg3eFczuylkak3O6tcoIoOe42JI3ihjWx6E/ww==", null, false, null, "f5530732-ec1c-44ba-abac-d006cc00a0da", "Egypt Standard Time", false, null, "MichaelW@303" },
                    { "5B91855C-2D98-4E2B-B919-CDE322C9002D", 0, "Fitness trainer and health coach.", null, null, "7a20d2cd-a2fe-452c-a8fd-15ed693f78c7", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 32, 928, DateTimeKind.Unspecified).AddTicks(8243), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1992, 7, 18), "emilyd@example.com", true, "Emily", "Female", "Davis", false, null, "EMILYD@EXAMPLE.COM", "EMILYD@202", "AQAAAAIAAYagAAAAEM+5wtJscOPCLzLCvvN8s0AGr6FVWPmKidqOuVd2IZpV0Wt6HRZTMfjNCszuTQDqYA==", null, false, null, "53ced8d3-808a-418e-951e-26304348c15f", "Egypt Standard Time", false, null, "EmilyD@202" },
                    { "702C7401-F83C-4684-9421-9AA74FC40050", 0, "Software Developer and Tech Enthusiast.", null, null, "9fc6fb1a-279a-40b4-99ab-8adf9a4866e3", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 32, 422, DateTimeKind.Unspecified).AddTicks(8088), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(2002, 1, 1), "me5260287@gmail.com", true, "Mohamed", "Male", "Ehab", false, null, "ME5260287@GMAIL.COM", "MOEHAB@2002", "AQAAAAIAAYagAAAAEEkVQav7uM/httOWGUq9uVr8FWNkvabpdEH4Mu+5OCA4Zuh34rLloz5TNN48XuNSgw==", null, false, null, "c96739b2-3f26-408f-85cc-72fd467d6d7e", "Egypt Standard Time", false, null, "Moehab@2002" },
                    { "9818FAE0-A167-4808-A30D-BC7418A53CB0", 0, "Passionate about art and design.", null, null, "d4eeae61-8c21-4a93-b7ef-fa5e6aa821a4", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 32, 650, DateTimeKind.Unspecified).AddTicks(9867), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1985, 8, 22), "janesmith@example.com", true, "Jane", "Female", "Smith", false, null, "JANESMITH@EXAMPLE.COM", "JANESMITH@456", "AQAAAAIAAYagAAAAEHgtGFwOgUVwdrYHyM1ANHOC7YPIikbrLLCbMewGU3v41bBgX23Wt8pbV/+xQAZApQ==", null, false, null, "f7665391-181b-4db7-b127-54936afe4a16", "Egypt Standard Time", false, null, "JaneSmith@456" },
                    { "B3945AB7-1F46-4829-9DEA-6860E283582F", 0, "Book lover and aspiring writer.", null, null, "885b75ad-eba3-4103-aa1c-aa93ec1a28b4", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 33, 122, DateTimeKind.Unspecified).AddTicks(9384), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1998, 4, 30), "sarahm@example.com", true, "Sarah", "Female", "Miller", false, null, "SARAHM@EXAMPLE.COM", "SARAHM@404", "AQAAAAIAAYagAAAAEAmrHAOGs6bRz1IzD0Br+RsStx5J1/y//Ay+Pb6JCMzZd6UWMb+XQi3WLl/o1ajPHA==", null, false, null, "e9c6391a-8a3a-41c6-a048-13347e484717", "Egypt Standard Time", false, null, "SarahM@404" },
                    { "FE2FB445-6562-49DD-B0A3-77E0A3A1C376", 0, "Travel enthusiast and foodie.", null, null, "9b7c6802-4a2f-4451-a5e2-348965a33308", null, new DateTimeOffset(new DateTime(2025, 2, 12, 12, 21, 32, 745, DateTimeKind.Unspecified).AddTicks(16), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1995, 3, 10), "alicej@example.com", true, "Alice", "Female", "Johnson", false, null, "ALICEJ@EXAMPLE.COM", "ALICEJ@789", "AQAAAAIAAYagAAAAEFinwmM2ZemEktvz+yxBTF9PKsF+FTkM7dmZ1gof8+cLQ69vUGkh1/CNhxkGHoYepg==", null, false, null, "952435d7-f36c-4971-b07a-e58f5daa3437", "Egypt Standard Time", false, null, "AliceJ@789" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "BE3B9D48-68F5-42E3-9371-E7964F96A25D", "049759F5-3AD8-46BF-89EE-AC51F3BEED88" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "0A9232F3-BC6D-4610-AAFF-F1032831E847" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "3944C201-0184-4F97-83A6-B6E4852C961F" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "3EB45CDA-F2EE-43E7-B9F1-D52562E05929" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "5326BB55-A26F-47FE-ABC4-9DF44F7B0333" },
                    { "BE3B9D48-68F5-42E3-9371-E7964F96A25D", "5B91855C-2D98-4E2B-B919-CDE322C9002D" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "702C7401-F83C-4684-9421-9AA74FC40050" },
                    { "BE3B9D48-68F5-42E3-9371-E7964F96A25D", "702C7401-F83C-4684-9421-9AA74FC40050" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "9818FAE0-A167-4808-A30D-BC7418A53CB0" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "B3945AB7-1F46-4829-9DEA-6860E283582F" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "FE2FB445-6562-49DD-B0A3-77E0A3A1C376" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_MessageId",
                table: "Attachments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ReceiverId",
                table: "Friendships",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupConversations_GroupId",
                table: "GroupConversations",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_AddedById",
                table: "GroupMembers",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserId",
                table: "GroupMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotifications_ActorId",
                table: "GroupNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotifications_GroupId",
                table: "GroupNotifications",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotifications_RecipientId",
                table: "GroupNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatedByUserId",
                table: "Groups",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinGroupRequests_GroupId",
                table: "JoinGroupRequests",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinGroupRequests_RequestorId",
                table: "JoinGroupRequests",
                column: "RequestorId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveStreams_UserId",
                table: "LiveStreams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaNotifications_ActorId",
                table: "MediaNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaNotifications_MediaId",
                table: "MediaNotifications",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaNotifications_RecipientId",
                table: "MediaNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageMentions_MentionedUserId",
                table: "MessageMentions",
                column: "MentionedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageMentions_MessageId",
                table: "MessageMentions",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_MessageId",
                table: "MessageReactions",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReactions_UserId",
                table: "MessageReactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReplies_CreatedAt",
                table: "MessageReplies",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReplies_OriginalMessageId",
                table: "MessageReplies",
                column: "OriginalMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReplies_ParentReplyId",
                table: "MessageReplies",
                column: "ParentReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReplies_RepliedById",
                table: "MessageReplies",
                column: "RepliedById");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GroupConversationId",
                table: "Messages",
                column: "GroupConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PrivateConversationId",
                table: "Messages",
                column: "PrivateConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkNotifications_ActorId",
                table: "NetworkNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkNotifications_RecipientId",
                table: "NetworkNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_ActorId",
                table: "PostNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_RecipientId",
                table: "PostNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateConversations_ReceiverUserId",
                table: "PrivateConversations",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateConversations_SenderUserId_ReceiverUserId",
                table: "PrivateConversations",
                columns: new[] { "SenderUserId", "ReceiverUserId" },
                unique: true,
                filter: "[SenderUserId] IS NOT NULL AND [ReceiverUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_UserId",
                table: "Stories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryComment_CommentedById",
                table: "StoryComment",
                column: "CommentedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoryComment_StoryId",
                table: "StoryComment",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryNotifications_ActorId",
                table: "StoryNotifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryNotifications_RecipientId",
                table: "StoryNotifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryNotifications_StoryId",
                table: "StoryNotifications",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryReaction_ReactedById",
                table: "StoryReaction",
                column: "ReactedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoryReaction_StoryId",
                table: "StoryReaction",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryViews_StoryId_ViewerId",
                table: "StoryViews",
                columns: new[] { "StoryId", "ViewerId" },
                unique: true,
                filter: "[StoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StoryViews_ViewerId",
                table: "StoryViews",
                column: "ViewerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowedUserId",
                table: "UserFollowers",
                column: "FollowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowerUserId",
                table: "UserFollowers",
                column: "FollowerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "GroupNotifications");

            migrationBuilder.DropTable(
                name: "JoinGroupRequests");

            migrationBuilder.DropTable(
                name: "LiveStreams");

            migrationBuilder.DropTable(
                name: "MediaNotifications");

            migrationBuilder.DropTable(
                name: "MessageMentions");

            migrationBuilder.DropTable(
                name: "MessageReactions");

            migrationBuilder.DropTable(
                name: "MessageReplies");

            migrationBuilder.DropTable(
                name: "NetworkNotifications");

            migrationBuilder.DropTable(
                name: "PostNotifications");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "StoryComment");

            migrationBuilder.DropTable(
                name: "StoryNotifications");

            migrationBuilder.DropTable(
                name: "StoryReaction");

            migrationBuilder.DropTable(
                name: "StoryViews");

            migrationBuilder.DropTable(
                name: "UserFollowers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "GroupConversations");

            migrationBuilder.DropTable(
                name: "PrivateConversations");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
