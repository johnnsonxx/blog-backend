namespace blog_backend.Shared.Passwords;

public interface IPasswordHashService
{
    string Hash(string password);

    bool Verify(string passwordHash, string password);
}
