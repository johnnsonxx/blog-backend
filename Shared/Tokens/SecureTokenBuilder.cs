using System.Security.Cryptography;

namespace blog_backend.Shared.Tokens;

public sealed class SecureTokenBuilder : ITokenBuilder
{
    public string Create(int byteLength = 32)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(byteLength, 16);
        return Convert.ToHexStringLower(RandomNumberGenerator.GetBytes(byteLength));
    }
}
