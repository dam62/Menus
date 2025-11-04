using CommunityToolkit.Mvvm.Input;
using Menus.Services;

namespace Menus.ViewModels;

public partial class ShopViewModel:ViewModelBase
{
    private NavigationService navigationService;
    public ShopViewModel(NavigationService navigationService)
    {
         this.navigationService  = navigationService;
    }

    public ShopViewModel()
    {
        
    }
    
    [RelayCommand]
    public void NavegateToInicio()
    {
        navigationService.NavigateTo(NavigationService.INICIO_VIEW);
    }
}