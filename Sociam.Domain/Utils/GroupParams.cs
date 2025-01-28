using Sociam.Domain.Enums;

namespace Sociam.Domain.Utils;

public sealed class GroupParams : BaseParams
{
    public string? Name { get; set; }
    public GroupPrivacy? GroupPrivacy { get; set; }
}