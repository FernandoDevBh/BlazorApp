using System.Text;

namespace API.Helper;

public class APISettings
{
    private byte[]? _secretKey;
    public required string SecretKey { get; set; }
    public required string ValidAudience { get; set; }
    public required string ValidIssuer { get; set; }

    public byte[] GetSecretKey()
    {
        return _secretKey ??= Encoding.UTF8.GetBytes(SecretKey);
    }
}
