using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using Menus.ViewModels;
using Menus.Views;

namespace Menus.Services;

public partial class NavigationService: ObservableObject
{
    public const string INICIO_VIEW = "inicio";
    public const string TIENDA_VIEW = "tienda";
    public const string EVENTO_VIEW = "evento";
    
    [ObservableProperty] private ContentControl currentView;
    
    [ObservableProperty] private NavigationViewItem selectedMenuItem;
    
    [ObservableProperty] private ObservableCollection<NavigationViewItem> menuItems = new();

    private NavigationViewItem itemShop;
    private NavigationViewItem itemHome;
    private NavigationViewItem itemEvent;

    public NavigationService()
    {
        itemHome = new NavigationViewItem
        {
            Content = "Inicio",
            Tag = INICIO_VIEW,
            IconSource = new SymbolIconSource{Symbol = Symbol.Home}
        };
        
        itemShop = new NavigationViewItem
        {
            Content = "Tienda",
            Tag = TIENDA_VIEW,
            IconSource = new SymbolIconSource{Symbol = Symbol.Shop}
        };
        itemEvent = new NavigationViewItem
        {
            Content = "Eventos",
            Tag = EVENTO_VIEW,
            IconSource = new SymbolIconSource{Symbol = Symbol.CalendarMonth}
        };
        
        MenuItems.Add(itemHome);
        MenuItems.Add(itemShop);
        MenuItems.Add(itemEvent);
        NavigateTo(INICIO_VIEW);
    }
    
    partial void OnSelectedMenuItemChanged(NavigationViewItem item)
    {
        NavigateTo(item.Tag.ToString());
    }
    
    public void NavigateTo(string tag)
    {
        if (tag.Equals(INICIO_VIEW))
        {
            HomeView homeView = new HomeView();
            homeView.DataContext = new HomeViewModel(this);
            CurrentView = homeView;
            SelectedMenuItem = itemHome;
        }
        else if (tag.Equals(TIENDA_VIEW))
        {
            ShopView shopView = new ShopView();
            shopView.DataContext = new ShopViewModel(this);
            CurrentView = shopView;
            SelectedMenuItem = itemShop;
        }
        else if (tag.Equals(EVENTO_VIEW))
        {
            EventView eventView = new EventView();
            eventView.DataContext = new EventViewModel(this);
            CurrentView = eventView;
            SelectedMenuItem = itemEvent;
        }
    }
}