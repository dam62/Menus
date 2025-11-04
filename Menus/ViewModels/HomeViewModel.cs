using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Menus.Services;

namespace Menus.ViewModels;

public partial class HomeViewModel:ViewModelBase
{
    private NavigationService _navigationService;
    
    [ObservableProperty] private bool isDialogOpen;
    
    [ObservableProperty] private bool isExitDialogOpen;
    
    public HomeViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public HomeViewModel()
    {
        
    }
    
    [RelayCommand]
    public void OpenDialog()
    {
        IsDialogOpen = !IsDialogOpen;
    }

    [RelayCommand]
    public void OpenExitDialog()
    {
        IsExitDialogOpen = true;
    }
    
    [RelayCommand]
    public void CloseExitDialog()
    {
        IsExitDialogOpen = false;
    }

    [RelayCommand]
    public void NavigateToTienda()
    {
        _navigationService.NavigateTo(NavigationService.TIENDA_VIEW);
    }
}