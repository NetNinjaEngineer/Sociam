using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;

namespace Sociam.Application.Features.Groups.Queries.GetAllGroups;

public sealed class GetAllGroupsQuery : IRequest<Result<IReadOnlyList<GroupListDto>>>;