using HttpModels;

namespace HttpApiServer_backend;

public interface ITokenService
{
    string GenerateToken(Account account);
}