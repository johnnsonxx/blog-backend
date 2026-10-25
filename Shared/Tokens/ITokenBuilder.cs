namespace blog_backend.Shared.Tokens;

public interface ITokenBuilder
{
    string Create(int byteLength = 32);
}
