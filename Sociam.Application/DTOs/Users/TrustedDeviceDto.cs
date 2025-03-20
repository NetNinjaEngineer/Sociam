namespace Sociam.Application.DTOs.Users;
public sealed record TrustedDeviceDto(
    Guid Id,
    string Browser,
    string Os,
    string IpAddress,
    string Location,
    bool IsActive,
    string FirstLogin,
    string Device,
    string LastActive);
