using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;
internal sealed class StoryViewConfiguration : IEntityTypeConfiguration<StoryView>
{
    public void Configure(EntityTypeBuilder<StoryView> builder)
    {
        builder.HasKey(sv => sv.Id);

        builder.HasIndex(sv => new { sv.StoryId, sv.ViewerId }).IsUnique();

        builder.HasOne(sv => sv.Story)
            .WithMany(s => s.StoryViewers)
            .HasForeignKey(sv => sv.StoryId)
            .OnDelete(deleteBehavior: DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.ToTable("StoryViews");
    }
}
