﻿using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Application.Features.Stories.Queries.GetExpiredStories;
public sealed class GetExpiredStoriesQuery : IRequest<Result<PagedResult<StoryViewsResponseDto>>>
{
    public StoryQueryParameters? QueryParameters { get; set; }
}
