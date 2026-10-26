using blog_backend.Entities;

namespace blog_backend.Shared.Tokens;

public interface IAccessTokenService
{
    AccessTokenResult Create(User user);
}
