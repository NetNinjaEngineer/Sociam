﻿namespace Sociam.Application.DTOs.Auth;
public sealed record RegisterResponseDto(string UserId, bool IsActivateRequired);