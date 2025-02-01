using Logiware.Application.Interfaces;

namespace Logiware.API.Configuration;

public class TokenSettings : ITokenSettings
{
    public string TokenKey { get; }
    
    public TokenSettings(IConfiguration configuration)
    {
        TokenKey = configuration["TokenKey"];
    }
}