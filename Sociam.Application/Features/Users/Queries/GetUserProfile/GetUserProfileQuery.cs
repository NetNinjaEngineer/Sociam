using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;

namespace Sociam.Application.Features.Users.Queries.GetUserProfile;
public sealed class GetUserProfileQuery : IRequest<Result<ProfileDto?>>
{
}
