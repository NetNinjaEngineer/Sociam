using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Infrastructure.Persistence;
public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<PrivateConversation> PrivateConversations { get; set; }
    public DbSet<GroupConversation> GroupConversations { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<StoryView> StoryViews { get; set; }
    public DbSet<LiveStream> LiveStreams { get; set; }
    public DbSet<UserFollower> UserFollowers { get; set; }
    public DbSet<MessageReaction> MessageReactions { get; set; }
    public DbSet<MessageMention> MessageMentions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
