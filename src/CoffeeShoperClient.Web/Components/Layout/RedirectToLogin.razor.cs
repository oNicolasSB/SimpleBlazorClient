using Microsoft.AspNetCore.Components;

namespace CoffeeShoperClient.Web.Components.Pages;

public partial class RedirectToLogin : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo($"/login?redirectUri={Uri.EscapeDataString(NavigationManager.Uri)}", true);
    }
}
