using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "text", nullable: true),
                    CoverPhotoUrl = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CodeExpiration = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    TimeZoneId = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequesterId = table.Column<string>(type: "text", nullable: false),
                    ReceiverId = table.Column<string>(type: "text", nullable: false),
                    FriendshipStatus = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    ActorId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ActionUrl = table.Column<string>(type: "text", nullable: true),
                    GroupId = table.Column<string>(type: "text", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    GroupRole = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    GroupPrivacy = table.Column<string>(type: "text", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: false),
                    PictureName = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsLive = table.Column<bool>(type: "boolean", nullable: false),
                    ViewerCount = table.Column<int>(type: "integer", nullable: false),
                    RecordingUrl = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    ActorId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ActionUrl = table.Column<string>(type: "text", nullable: true),
                    MediaId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaNotificationType = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    ActorId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ActionUrl = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    ActorId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ActionUrl = table.Column<string>(type: "text", nullable: true),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostContent = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastMessageAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SenderUserId = table.Column<string>(type: "text", nullable: false),
                    ReceiverUserId = table.Column<string>(type: "text", nullable: false)
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
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RevokedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    StoryPrivacy = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    StoryType = table.Column<string>(type: "VARCHAR(5)", maxLength: 5, nullable: false),
                    Caption = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MediaUrl = table.Column<string>(type: "text", nullable: true),
                    MediaType = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    HashTags = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    ActorId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReadAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ActionUrl = table.Column<string>(type: "text", nullable: true),
                    StoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Privacy = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FollowedUserId = table.Column<string>(type: "text", nullable: false),
                    FollowerUserId = table.Column<string>(type: "text", nullable: false),
                    FollowedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastMessageAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    JoinedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    AddedById = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RequestorId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CommentedById = table.Column<string>(type: "text", nullable: false),
                    CommentedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReactedById = table.Column<string>(type: "text", nullable: false),
                    ReactedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReactionType = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    ViewerId = table.Column<string>(type: "text", nullable: false),
                    IsViewed = table.Column<bool>(type: "boolean", nullable: false),
                    ViewedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PrivateConversationId = table.Column<Guid>(type: "uuid", nullable: true),
                    GroupConversationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ReadedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    MessageStatus = table.Column<string>(type: "text", nullable: false),
                    IsEdited = table.Column<bool>(type: "boolean", nullable: false),
                    IsPinned = table.Column<bool>(type: "boolean", nullable: false),
                    SenderId = table.Column<string>(type: "text", nullable: false),
                    ReceiverId = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AttachmentSize = table.Column<long>(type: "bigint", nullable: false),
                    AttachmentType = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    MentionedUserId = table.Column<string>(type: "text", nullable: false),
                    MentionType = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ReactionType = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsEdited = table.Column<bool>(type: "boolean", nullable: false),
                    EditedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RepliedById = table.Column<string>(type: "text", nullable: false),
                    ReplyStatus = table.Column<string>(type: "text", nullable: false),
                    ParentReplyId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    { "049759F5-3AD8-46BF-89EE-AC51F3BEED88", 0, "Gamer and tech enthusiast.", null, null, "a4d029b3-b29b-4935-8450-718543a56c19", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 10, 353, DateTimeKind.Unspecified).AddTicks(2409), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1985, 2, 14), "chrisa@example.com", true, "Chris", "Male", "Anderson", false, null, "CHRISA@EXAMPLE.COM", "CHRISA@707", "AQAAAAIAAYagAAAAEJTWA8/QrwJgtCjvVP/lscvaJFh0pYZCTOgWdFYe1aU58lSO+b83bAhtUPwB4tfsDw==", null, false, null, "f141de03-7b6e-4420-a287-6e3bb55f37f7", "Egypt Standard Time", false, null, "ChrisA@707" },
                    { "0821819C-64AE-4C73-96F2-4E607AA59D7E", 0, "Tech entrepreneur and mentor.", null, null, "2ef2c03b-dc05-4cf1-99ef-158b8e0c9969", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 9, 809, DateTimeKind.Unspecified).AddTicks(6458), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1980, 12, 5), "bobbrown@example.com", true, "Bob", "Male", "Brown", false, null, "BOBBROWN@EXAMPLE.COM", "BOBBROWN@101", "AQAAAAIAAYagAAAAEML5DvJUVZ3QFHrXwXzg7bfUAyLqfP6b64k/ya7PP2H8haiY6SEyVgG1yjj0l8B54A==", null, false, null, "7a1c6888-63ad-48cb-8431-d680bfb09254", "Egypt Standard Time", false, null, "BobBrown@101" },
                    { "0A9232F3-BC6D-4610-AAFF-F1032831E847", 0, "Nature lover and environmentalist.", null, null, "60147f15-9db7-49b2-9561-a8cd06f260d0", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 10, 260, DateTimeKind.Unspecified).AddTicks(9140), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1990, 6, 20), "laurat@example.com", true, "Laura", "Female", "Taylor", false, null, "LAURAT@EXAMPLE.COM", "LAURAT@606", "AQAAAAIAAYagAAAAEMXs6jRvapBAVlv7dtw/5EK6XhWlC7ElhUZ9MvIjb1d7aIikjpHw6xTfoBZniV4wOg==", null, false, null, "a791665a-374a-491d-b8ad-11f1c456518e", "Egypt Standard Time", false, null, "LauraT@606" },
                    { "3944C201-0184-4F97-83A6-B6E4852C961F", 0, "History buff and teacher.", null, null, "3724f023-0d2d-4013-b2ec-e53f2273a1fb", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 10, 170, DateTimeKind.Unspecified).AddTicks(2803), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1975, 11, 12), "davidm@example.com", true, "David", "Male", "Moore", false, null, "DAVIDM@EXAMPLE.COM", "DAVIDM@505", "AQAAAAIAAYagAAAAEIAn7cU8lN/wip0AfOKb9vhFBqSCa8SLcd94AC17H7bRP+RiF+6+F9MUw0FNLE9i3Q==", null, false, null, "1b8f3b85-58c1-4f54-8345-b1cfb0dfdf9e", "Egypt Standard Time", false, null, "DavidM@505" },
                    { "3EB45CDA-F2EE-43E7-B9F1-D52562E05929", 0, "Loves hiking and photography.", null, null, "1ae6f082-45c9-4bd9-bdf4-57b1c146b83c", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 9, 535, DateTimeKind.Unspecified).AddTicks(4952), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1990, 5, 15), "johndoe@example.com", true, "John", "Male", "Doe", false, null, "JOHNDOE@EXAMPLE.COM", "JOHNDOE@123", "AQAAAAIAAYagAAAAECkwWpdlp704lvsZBW8cONS++cyLNNyTxzkyODJKoK6VppDwBOg8N3NG2HC6nWGDuA==", null, false, null, "5199cacf-1ab0-421b-9dcf-4c77cf00d1ee", "Egypt Standard Time", false, null, "JohnDoe@123" },
                    { "5326BB55-A26F-47FE-ABC4-9DF44F7B0333", 0, "Musician and songwriter.", null, null, "2fb1982d-b05a-432a-b3dc-4a21ce75ef1b", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 9, 990, DateTimeKind.Unspecified).AddTicks(2238), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1988, 9, 25), "michaelw@example.com", true, "Michael", "Male", "Wilson", false, null, "MICHAELW@EXAMPLE.COM", "MICHAELW@303", "AQAAAAIAAYagAAAAEEgoAVu16vCw5TmNULO0O4PzJzm4Bd7DLhwbxHuHv0SJDOr+X8l8lyu0grCuQWmeqw==", null, false, null, "63b89f54-7af8-4b32-bf8b-317436ee363a", "Egypt Standard Time", false, null, "MichaelW@303" },
                    { "5B91855C-2D98-4E2B-B919-CDE322C9002D", 0, "Fitness trainer and health coach.", null, null, "2af191d4-335c-42c4-9c2d-67898a4f4675", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 9, 900, DateTimeKind.Unspecified).AddTicks(8806), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1992, 7, 18), "emilyd@example.com", true, "Emily", "Female", "Davis", false, null, "EMILYD@EXAMPLE.COM", "EMILYD@202", "AQAAAAIAAYagAAAAEFo/ZArw7JoEwp8NwiUJUq4nFUsPKLhCo532lt1t20FeJop1tJZJT+VZbQh2DmzSJA==", null, false, null, "d36012bd-9e23-43d2-a725-cd9d97dd70ae", "Egypt Standard Time", false, null, "EmilyD@202" },
                    { "702C7401-F83C-4684-9421-9AA74FC40050", 0, "Software Developer and Tech Enthusiast.", null, null, "432d545d-b45e-46ad-bcdc-554ff3ccd18d", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 9, 436, DateTimeKind.Unspecified).AddTicks(8644), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(2002, 1, 1), "me5260287@gmail.com", true, "Mohamed", "Male", "Ehab", false, null, "ME5260287@GMAIL.COM", "MOEHAB@2002", "AQAAAAIAAYagAAAAEH6uo4sbOHirQ3V9vcosdPy3p0vyLM856IOjiPpexMS3RvSQMfY4PjhqduGYXY7Gyw==", null, false, null, "e66b4aea-98a0-45af-8784-beb6ba6a81a7", "Egypt Standard Time", false, null, "Moehab@2002" },
                    { "9818FAE0-A167-4808-A30D-BC7418A53CB0", 0, "Passionate about art and design.", null, null, "74133ec0-d091-4a55-93bd-14e9831829ac", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 9, 627, DateTimeKind.Unspecified).AddTicks(2813), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1985, 8, 22), "janesmith@example.com", true, "Jane", "Female", "Smith", false, null, "JANESMITH@EXAMPLE.COM", "JANESMITH@456", "AQAAAAIAAYagAAAAECLdpQGQR9HR0q1IOnx/zpOIUxkw8zw+oEcxyhiXpzybOb9VTmXj6KS1n/AS+l8rsA==", null, false, null, "ecd870bd-bb0c-42ac-8cc0-d3feab2e4d67", "Egypt Standard Time", false, null, "JaneSmith@456" },
                    { "B3945AB7-1F46-4829-9DEA-6860E283582F", 0, "Book lover and aspiring writer.", null, null, "313965f1-c6ad-403e-b4a5-a5dbff829120", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 10, 80, DateTimeKind.Unspecified).AddTicks(5499), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1998, 4, 30), "sarahm@example.com", true, "Sarah", "Female", "Miller", false, null, "SARAHM@EXAMPLE.COM", "SARAHM@404", "AQAAAAIAAYagAAAAEJKOlD8HThPosvvegMYDCFHbS5uypre34BXexJoo1p2IrwFSGktXjkF062hxno/4vQ==", null, false, null, "4c9bd332-1e1f-4a0a-8fca-6b0dbafe1643", "Egypt Standard Time", false, null, "SarahM@404" },
                    { "FE2FB445-6562-49DD-B0A3-77E0A3A1C376", 0, "Travel enthusiast and foodie.", null, null, "fad0a0c6-5e57-4714-af99-221ee6ecfc5f", null, new DateTimeOffset(new DateTime(2025, 2, 14, 22, 49, 9, 720, DateTimeKind.Unspecified).AddTicks(2506), new TimeSpan(0, 2, 0, 0, 0)), new DateOnly(1995, 3, 10), "alicej@example.com", true, "Alice", "Female", "Johnson", false, null, "ALICEJ@EXAMPLE.COM", "ALICEJ@789", "AQAAAAIAAYagAAAAEFpiTJ/ahAJ74mP11mLFkylvGEIrd8HToXBLRYHyoXD6Uunh0FJDU1PWbWfc0dMQ5A==", null, false, null, "2277a7f6-a222-4345-8bf8-88abda41d752", "Egypt Standard Time", false, null, "AliceJ@789" }
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
                unique: true);

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
                unique: true);

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
                unique: true);

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
                unique: true);

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
