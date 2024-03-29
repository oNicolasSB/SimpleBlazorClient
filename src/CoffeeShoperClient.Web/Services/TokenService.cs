using CoffeeShoperClient.Web.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace CoffeeShoperClient.Web.Services;

public class TokenService : ITokenService
{
    public readonly IOptions<IdentityServerSettings> identityServerSettings;
    public readonly DiscoveryDocumentResponse discoveryDocument;
    private readonly HttpClient _httpClient;

    public TokenService(IOptions<IdentityServerSettings> identityServerSettings)
    {
        this.identityServerSettings = identityServerSettings;
        _httpClient = new HttpClient();
        discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(this.identityServerSettings.Value.DiscoveryUrl).Result;

        if (discoveryDocument.IsError)
        {
            throw new Exception("Unable to get discovery document", discoveryDocument.Exception);
        }
    }

    public async Task<TokenResponse> GetToken(string scope)
    {
        var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = identityServerSettings.Value.ClientName,
            ClientSecret = identityServerSettings.Value.ClientPassword,
            Scope = scope
        });

        if (discoveryDocument.IsError)
        {
            throw new Exception("Unable to get discovery document", discoveryDocument.Exception);
        }

        return tokenResponse;
    }
}