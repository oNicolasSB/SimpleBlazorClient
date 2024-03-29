using CoffeeShoperClient.Web.Components.Data;
using CoffeeShoperClient.Web.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace CoffeeShoperClient.Web.Components.Pages;
[Authorize]
public partial class CoffeShops : ComponentBase
{
    private List<CoffeShopModel> Shops = [];
    [Inject] private HttpClient HttpClient { get; set; }
    [Inject] private IConfiguration Configuration { get; set; }

    [Inject] private ITokenService TokenService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var tokenResponse = await TokenService.GetToken("CoffeeeApi.read");
        HttpClient.SetBearerToken(tokenResponse.AccessToken);
        var result = await HttpClient.GetAsync(Configuration["ApiUrl"] + "/api/CoffeeShop");
        if (result.IsSuccessStatusCode)
        {
            Shops = await result.Content.ReadFromJsonAsync<List<CoffeShopModel>>();
        }
    }
}
