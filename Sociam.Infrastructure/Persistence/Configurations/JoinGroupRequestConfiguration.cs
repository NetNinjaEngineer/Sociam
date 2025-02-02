using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations
{
    public sealed class JoinGroupRequestConfiguration : IEntityTypeConfiguration<JoinGroupRequest>
    {
        public void Configure(EntityTypeBuilder<JoinGroupRequest> builder)
        {
            builder.HasKey(request => request.Id);

            builder.Property(request => request.Id).ValueGeneratedNever();

            builder.HasOne(request => request.Group)
                .WithMany(group => group.JoinGroupRequests)
                .HasForeignKey(request => request.GroupId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(request => request.Requestor)
                .WithMany(requestor => requestor.JoinGroupRequests)
                .HasForeignKey(request => request.RequestorId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(request => request.Status)
                .HasConversion(
                    joinRequestStatus => joinRequestStatus.ToString(),
                    value => (JoinRequestStatus)Enum.Parse(typeof(JoinRequestStatus), value));


            builder.ToTable("JoinGroupRequests");
        }
    }
}
