using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Groups
{
    public sealed class GroupDto : GroupListDto
    {
        public GroupPrivacy Privacy { get; set; }
    }
}
