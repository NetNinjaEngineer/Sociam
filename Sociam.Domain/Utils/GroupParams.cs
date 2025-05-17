using Sociam.Domain.Enums;

namespace Sociam.Domain.Utils;

public sealed class GroupParams : SearchableParams
{
    public string? Name { get; set; }
    public GroupPrivacy? GroupPrivacy { get; set; }
    public bool EnablePaging { get; set; }
}