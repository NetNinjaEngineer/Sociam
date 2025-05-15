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
                    DeviceVerificationCode = table.Column<string>(type: "text", nullable: true),
                    DeviceVerificationExpiry = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    TimeZoneId = table.Column<string>(type: "text", nullable: false),
                    LastKnownIp = table.Column<string>(type: "text", nullable: true),
                    LastKnownLocation = table.Column<string>(type: "text", nullable: true),
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
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    GroupPrivacy = table.Column<string>(type: "text", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: false),
                    PictureName = table.Column<string>(type: "text", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                name: "Notifications",
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
                    NKind = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    GroupId = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    GroupRole = table.Column<string>(type: "text", nullable: true),
                    MediaId = table.Column<Guid>(type: "uuid", nullable: true),
                    MediaNotificationType = table.Column<string>(type: "text", nullable: true),
                    PostId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostContent = table.Column<string>(type: "text", nullable: true),
                    StoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Privacy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    SharesCount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Privacy = table.Column<string>(type: "text", nullable: false),
                    OriginalPostId = table.Column<Guid>(type: "uuid", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Feeling = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_OriginalPostId",
                        column: x => x.OriginalPostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "TrustedDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    DeviceId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    OsPlatform = table.Column<string>(type: "text", nullable: false),
                    OsName = table.Column<string>(type: "text", nullable: false),
                    OsVersion = table.Column<string>(type: "text", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastLogin = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    BrowserName = table.Column<string>(type: "text", nullable: false),
                    BrowserVersion = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustedDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrustedDevices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastMessageAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ConversationType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    SenderUserId = table.Column<string>(type: "text", nullable: true),
                    ReceiverUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversations_Groups_GroupId",
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
                name: "PostComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostComments_PostComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "PostComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostComments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    MediaType = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetId = table.Column<string>(type: "text", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PostReactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReactedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ReactedById = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostReactions_AspNetUsers_ReactedById",
                        column: x => x.ReactedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaggedUserId = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTags_AspNetUsers_TaggedUserId",
                        column: x => x.TaggedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ReadedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    MessageStatus = table.Column<string>(type: "text", nullable: false),
                    IsEdited = table.Column<bool>(type: "boolean", nullable: false),
                    IsPinned = table.Column<bool>(type: "boolean", nullable: false),
                    SenderId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "Id", "AccessFailedCount", "Bio", "Code", "CodeExpiration", "ConcurrencyStamp", "CoverPhotoUrl", "CreatedAt", "DateOfBirth", "DeviceVerificationCode", "DeviceVerificationExpiry", "Email", "EmailConfirmed", "FirstName", "Gender", "LastKnownIp", "LastKnownLocation", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TimeZoneId", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { "049759F5-3AD8-46BF-89EE-AC51F3BEED88", 0, "Gamer and tech enthusiast.", null, null, "2cbc1cca-a5e3-43ed-af11-c83b38d41ec5", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 27, 873, DateTimeKind.Unspecified).AddTicks(3875), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1985, 2, 14), null, null, "chrisa@example.com", true, "Chris", "Male", null, null, "Anderson", false, null, "CHRISA@EXAMPLE.COM", "CHRISA@707", "AQAAAAIAAYagAAAAEAlCkCwe6yOVbMlLU9bGIcieBPhW5M3ngIEqED3Q4JeAwPlgtqhKQNms110slq7FZA==", null, false, null, "37e83e81-4307-4aba-b0d2-96ce960129eb", "Egypt Standard Time", false, null, "ChrisA@707" },
                    { "0821819C-64AE-4C73-96F2-4E607AA59D7E", 0, "Tech entrepreneur and mentor.", null, null, "345b0171-d9c7-4aa8-ba71-c2bc25c2c95d", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 27, 195, DateTimeKind.Unspecified).AddTicks(4897), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1980, 12, 5), null, null, "bobbrown@example.com", true, "Bob", "Male", null, null, "Brown", false, null, "BOBBROWN@EXAMPLE.COM", "BOBBROWN@101", "AQAAAAIAAYagAAAAEPnEZpLsB+M1zY8wyjGo19ExDH9cG+xOKUmrOnNiDeWn4fI+t8bl+l6fmWAu36JKwA==", null, false, null, "58bfdca2-c4cb-4384-ac8a-c7e7803d1e36", "Egypt Standard Time", false, null, "BobBrown@101" },
                    { "0A9232F3-BC6D-4610-AAFF-F1032831E847", 0, "Nature lover and environmentalist.", null, null, "85b2358a-7d2b-4c3c-b985-d7a23a6a8847", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 27, 774, DateTimeKind.Unspecified).AddTicks(1051), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1990, 6, 20), null, null, "laurat@example.com", true, "Laura", "Female", null, null, "Taylor", false, null, "LAURAT@EXAMPLE.COM", "LAURAT@606", "AQAAAAIAAYagAAAAELArO93w57/okGGLR9q+X9Eer7usjCNFGuktG97r+XkVK0Kg8zOSVYkB3XNImX1DZQ==", null, false, null, "fdd25685-941d-49ee-bd3e-6697cd86c514", "Egypt Standard Time", false, null, "LauraT@606" },
                    { "3944C201-0184-4F97-83A6-B6E4852C961F", 0, "History buff and teacher.", null, null, "0d17b79a-d8d2-4131-9559-dc77a9bf9ca1", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 27, 640, DateTimeKind.Unspecified).AddTicks(645), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1975, 11, 12), null, null, "davidm@example.com", true, "David", "Male", null, null, "Moore", false, null, "DAVIDM@EXAMPLE.COM", "DAVIDM@505", "AQAAAAIAAYagAAAAEFNCxH5tagj6266HDbFMluN8Fc+l+ks4ocs1cPD6PyxPCzBLdc8txPphSI+xmEtIXQ==", null, false, null, "4d673954-dbb6-4824-aa46-10a5b50ebb8a", "Egypt Standard Time", false, null, "DavidM@505" },
                    { "3EB45CDA-F2EE-43E7-B9F1-D52562E05929", 0, "Loves hiking and photography.", null, null, "e34f2faf-43c3-4629-b1cf-99250b72a6eb", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 26, 861, DateTimeKind.Unspecified).AddTicks(89), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1990, 5, 15), null, null, "johndoe@example.com", true, "John", "Male", null, null, "Doe", false, null, "JOHNDOE@EXAMPLE.COM", "JOHNDOE@123", "AQAAAAIAAYagAAAAEFxQCq79/1CqBICeuvp/uekxp27byJWWvRPwIUWlpz3bWYUamaVEX17NyZQXDvLhFQ==", null, false, null, "4078127f-b052-4ddb-aa94-4ff45b5ca1e5", "Egypt Standard Time", false, null, "JohnDoe@123" },
                    { "5326BB55-A26F-47FE-ABC4-9DF44F7B0333", 0, "Musician and songwriter.", null, null, "ac1de7b9-6469-46b3-ac0e-634352212a9a", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 27, 422, DateTimeKind.Unspecified).AddTicks(311), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1988, 9, 25), null, null, "michaelw@example.com", true, "Michael", "Male", null, null, "Wilson", false, null, "MICHAELW@EXAMPLE.COM", "MICHAELW@303", "AQAAAAIAAYagAAAAEBosMHIhlwMW2OW79+zF19ET4Dzst5byd++Nb3WfIfFExLGOQxqSs2JzRR+xT8tjTA==", null, false, null, "c9d7fb79-6d72-429a-a32f-8c2a9e5bce8a", "Egypt Standard Time", false, null, "MichaelW@303" },
                    { "5B91855C-2D98-4E2B-B919-CDE322C9002D", 0, "Fitness trainer and health coach.", null, null, "3fe3ea2f-5221-40e6-8e7c-6e39393f9ebf", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 27, 307, DateTimeKind.Unspecified).AddTicks(4590), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1992, 7, 18), null, null, "emilyd@example.com", true, "Emily", "Female", null, null, "Davis", false, null, "EMILYD@EXAMPLE.COM", "EMILYD@202", "AQAAAAIAAYagAAAAEJqwFP6rC0L9mKsRqCdt1Z7ao62SrwlNXg5JKvR7LJO57LIsaQz64ZU0C8Tb0levZw==", null, false, null, "a8a673ce-3932-4657-8c55-afba0cee8b6d", "Egypt Standard Time", false, null, "EmilyD@202" },
                    { "702C7401-F83C-4684-9421-9AA74FC40050", 0, "Software Developer and Tech Enthusiast.", null, null, "60587fe6-10f0-4f6c-ac4b-2fa0443c3cb4", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 26, 745, DateTimeKind.Unspecified).AddTicks(5431), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(2002, 1, 1), null, null, "me5260287@gmail.com", true, "Mohamed", "Male", null, null, "Ehab", false, null, "ME5260287@GMAIL.COM", "MOEHAB@2002", "AQAAAAIAAYagAAAAELqM6xt8XBwGv8UckBD+nG1Ce3riY2j2mElStCPo/4g9MIHcWf/jmSyPiAiN5few0Q==", null, false, null, "22d1d334-8876-414f-b5e5-572593cfa7be", "Egypt Standard Time", false, null, "Moehab@2002" },
                    { "9818FAE0-A167-4808-A30D-BC7418A53CB0", 0, "Passionate about art and design.", null, null, "1dfa6fc8-6e2e-4d1d-b0f1-c0e6f0ca0149", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 26, 960, DateTimeKind.Unspecified).AddTicks(3447), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1985, 8, 22), null, null, "janesmith@example.com", true, "Jane", "Female", null, null, "Smith", false, null, "JANESMITH@EXAMPLE.COM", "JANESMITH@456", "AQAAAAIAAYagAAAAEFviWx/APUbXyiY8eHCHHqp86VK/2OD2YjHtSbBYFTJg00pBSNEKi0dqwSR91hoCHA==", null, false, null, "a929e620-0b62-4ee3-b442-cdd05204ceae", "Egypt Standard Time", false, null, "JaneSmith@456" },
                    { "B3945AB7-1F46-4829-9DEA-6860E283582F", 0, "Book lover and aspiring writer.", null, null, "149690d0-def6-42c7-a809-f06306386d4f", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 27, 522, DateTimeKind.Unspecified).AddTicks(3355), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1998, 4, 30), null, null, "sarahm@example.com", true, "Sarah", "Female", null, null, "Miller", false, null, "SARAHM@EXAMPLE.COM", "SARAHM@404", "AQAAAAIAAYagAAAAEABKvXV27eDTI723xY0x9nlutNLFcm3hEVeXmttkkD/nFNfnNGyOUiwFbUQgjEMa9Q==", null, false, null, "ee8e602a-288a-4025-b217-76b1f7750f76", "Egypt Standard Time", false, null, "SarahM@404" },
                    { "FE2FB445-6562-49DD-B0A3-77E0A3A1C376", 0, "Travel enthusiast and foodie.", null, null, "d86b0398-0bcd-4ced-b751-8f791deedaea", null, new DateTimeOffset(new DateTime(2025, 5, 15, 19, 16, 27, 90, DateTimeKind.Unspecified).AddTicks(3830), new TimeSpan(0, 3, 0, 0, 0)), new DateOnly(1995, 3, 10), null, null, "alicej@example.com", true, "Alice", "Female", null, null, "Johnson", false, null, "ALICEJ@EXAMPLE.COM", "ALICEJ@789", "AQAAAAIAAYagAAAAEBgHZQ7ctRP69Hy7WS5i9a9ihIjk5s3n8xpb6vmBKXxHdx/pZfvK5opTk/Wli0iP/A==", null, false, null, "2ee5dbd0-fab5-4f4e-8632-3b3f7396854b", "Egypt Standard Time", false, null, "AliceJ@789" }
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
                name: "IX_Conversations_GroupId",
                table: "Conversations",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ReceiverUserId",
                table: "Conversations",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_SenderUserId_ReceiverUserId",
                table: "Conversations",
                columns: new[] { "SenderUserId", "ReceiverUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ReceiverId",
                table: "Friendships",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendships",
                column: "RequesterId");

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
                name: "IX_Groups_ApplicationUserId",
                table: "Groups",
                column: "ApplicationUserId");

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
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ActorId",
                table: "Notifications",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_GroupId",
                table: "Notifications",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MediaId",
                table: "Notifications",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostId",
                table: "Notifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientId",
                table: "Notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_StoryId",
                table: "Notifications",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_CreatedById",
                table: "PostComments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_ParentCommentId",
                table: "PostComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostMedia_PostId",
                table: "PostMedia",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_PostId",
                table: "PostReactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_ReactedById",
                table: "PostReactions",
                column: "ReactedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_OriginalPostId",
                table: "Posts",
                column: "OriginalPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TaggedUserId",
                table: "PostTags",
                column: "TaggedUserId");

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
                name: "IX_TrustedDevices_UserId",
                table: "TrustedDevices",
                column: "UserId");

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
                name: "JoinGroupRequests");

            migrationBuilder.DropTable(
                name: "LiveStreams");

            migrationBuilder.DropTable(
                name: "MessageMentions");

            migrationBuilder.DropTable(
                name: "MessageReactions");

            migrationBuilder.DropTable(
                name: "MessageReplies");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "PostMedia");

            migrationBuilder.DropTable(
                name: "PostReactions");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "StoryComment");

            migrationBuilder.DropTable(
                name: "StoryReaction");

            migrationBuilder.DropTable(
                name: "StoryViews");

            migrationBuilder.DropTable(
                name: "TrustedDevices");

            migrationBuilder.DropTable(
                name: "UserFollowers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
