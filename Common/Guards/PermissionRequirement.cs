using Microsoft.AspNetCore.Authorization;

namespace blog_backend.Common.Guards;

public sealed record PermissionRequirement(string Permission) : IAuthorizationRequirement;
