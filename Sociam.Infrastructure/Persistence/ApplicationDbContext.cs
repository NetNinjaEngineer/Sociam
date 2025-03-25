using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Infrastructure.Persistence;
public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<StoryView> StoryViews { get; set; }
    public DbSet<LiveStream> LiveStreams { get; set; }
    public DbSet<UserFollower> UserFollowers { get; set; }
    public DbSet<MessageReaction> MessageReactions { get; set; }
    public DbSet<MessageMention> MessageMentions { get; set; }
    public DbSet<MessageReply> MessageReplies { get; set; }
    public DbSet<JoinGroupRequest> JoinGroupRequests { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostComment> PostComments { get; set; }
    public DbSet<PostReaction> PostReactions { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.ConfigureWarnings(
            options => options.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}
