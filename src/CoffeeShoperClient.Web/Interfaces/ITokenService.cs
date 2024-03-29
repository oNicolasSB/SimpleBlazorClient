using IdentityModel.Client;

namespace CoffeeShoperClient.Web.Interfaces;

public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}