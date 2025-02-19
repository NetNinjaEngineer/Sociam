using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class PostNotificationConfiguration : IEntityTypeConfiguration<PostNotification>
{
    public void Configure(EntityTypeBuilder<PostNotification> builder)
    {
        builder.HasIndex(x => x.PostId);
    }
}