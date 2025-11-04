using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using Menus.Services;
using Menus.Views;

namespace Menus.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private NavigationService navigationService = new();

    public MainViewModel()
    {
        
    }
}