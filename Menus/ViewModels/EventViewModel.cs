using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarfBuzzSharp;
using Menus.Models;
using Menus.Services;

namespace Menus.ViewModels;

public partial class EventViewModel:ViewModelBase
{
    private NavigationService navigationService;

    [ObservableProperty] ObservableCollection<string> provincias = new();
    [ObservableProperty] ObservableCollection<string> municipios = new();
    [ObservableProperty] private string selectedProvincia;

    [ObservableProperty] private bool isLanguageSelected;
    [ObservableProperty] private bool isEntornoSelected;
    
    [ObservableProperty] private int pageIndex = 0;
    [ObservableProperty] private bool isReverse = false;
    
    [ObservableProperty] private string mensaje = string.Empty;
    
    //Lista de lenguajes
    [ObservableProperty] private ObservableCollection<LanguageModel> languageList = new();
    //[ObservableProperty] private ObservableCollection<LanguageModel> selectedLanguages = new();
    [ObservableProperty] private RegisterModel registerModel = new();
    
    //Lista de entornos
    [ObservableProperty] private ObservableCollection<LanguageModel> idsList = new();
    [ObservableProperty] private ObservableCollection<LanguageModel> selectedIds = new();
    
    public EventViewModel(NavigationService navigationService)
    {
        this.navigationService = navigationService;
        LoadLanguageList();
        LoadEntornos();
        _ = ObtenerProvincias();
    }

    public EventViewModel()
    {
        
    }

    partial void OnSelectedProvinciaChanged(string value)
    {
        _ = ObtenerMunicipio(value);
    }

    private async Task ObtenerProvincias()
    {
        string url = "https://public.opendatasoft.com/api/records/1.0/search/?dataset=georef-spain-provincia&rows=60&fields=prov_name,prov_code,acom_name&sort=prov_name&format=json";
        using HttpClient client = new();
        string json = await client.GetStringAsync(url);
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        foreach (var record in jsonDocument.RootElement.GetProperty("records").EnumerateArray())
        {
            var fields = record.GetProperty("fields");
            string provincia = fields.GetProperty("prov_name").GetString();
            
            Provincias.Add(provincia);
        }
    }

    private async Task ObtenerMunicipio(string provincia)
    {
        Municipios = new();
        string url =  $"https://public.opendatasoft.com/api/records/1.0/search/?dataset=georef-spain-municipio&refine.prov_name={Uri.EscapeDataString(provincia)}&fields=mun_name,prov_name&rows=1000&sort=mun_name&format=json";
        using HttpClient client = new();
        string json = await client.GetStringAsync(url);
        using JsonDocument jsonDocument = JsonDocument.Parse(json);
        foreach (var record in jsonDocument.RootElement.GetProperty("records").EnumerateArray())
        {
            var fields = record.GetProperty("fields");
            string municipio = fields.GetProperty("mun_name").GetString();
            Municipios.Add(municipio);
        }
    }
    
    [RelayCommand]
    public void SelectedLanguagesChanged()
    {
        if (RegisterModel.selectedLanguages.Count == 0)
        {
            IsLanguageSelected = false;
        }
        else
        {
            IsLanguageSelected = true;
        }
        
        if (RegisterModel.selectedLanguages.Count == 5)
        {
            RegisterModel.selectedLanguages.Remove(RegisterModel.selectedLanguages.Last());
            return;
        }
        Mensaje = "HAS SELECCIONADO: " + RegisterModel.selectedLanguages.Count + "/4";
    }
    
    [RelayCommand]
    public void SelectedEntornoChanged()
    {
        if (SelectedIds.Count == 0)
        {
            IsEntornoSelected = false;
        }
        else
        {
            IsEntornoSelected = true;
        }
    }
    
    [RelayCommand]
    public void NavegateTo(string tag)
    {
        navigationService.NavigateTo(tag);
    }
    
    [RelayCommand]
    public void CambiarTab(string pageIndex)
    {
        if (pageIndex.Equals("1"))
        {
            PageIndex ++;
            IsReverse = false;
        }
        else
        {
            PageIndex --;
            IsReverse = true;
        }
        
    }

    private void LoadLanguageList()
    {
        var uri = new Uri("avares://Menus/Assets/Languages/python.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "Python"});
        
        uri = new Uri("avares://Menus/Assets/Languages/C++.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "C++"});
        
        uri = new Uri("avares://Menus/Assets/Languages/cobol.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "Cobol"});
        
        uri = new Uri("avares://Menus/Assets/Languages/Csharp.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "C#"});
        
        uri = new Uri("avares://Menus/Assets/Languages/C.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "C"});
        
        uri = new Uri("avares://Menus/Assets/Languages/Java.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "Java"});
        
        uri = new Uri("avares://Menus/Assets/Languages/javascript.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "JavaScript"});
        
        uri = new Uri("avares://Menus/Assets/Languages/kotlin.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "Kotlin"});
        
        uri = new Uri("avares://Menus/Assets/Languages/SQL.png");
        LanguageList.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(uri)), Nombre = "SQL"});
    }

    private void LoadItems(Uri urlImage, string nombre, ObservableCollection<LanguageModel> lista)
    {
        lista.Add(new LanguageModel(){ImagenPath = new Bitmap(AssetLoader.Open(urlImage)), Nombre = nombre});
    }

    private void LoadEntornos()
    {
        LoadItems(new Uri("avares://Menus/Assets/Entornos/eclipse.png"), "Eclipse", IdsList);
        LoadItems(new Uri("avares://Menus/Assets/Entornos/IntelliJ.png"), "IntelliJ", IdsList);
        LoadItems(new Uri("avares://Menus/Assets/Entornos/jetbrains.Rider.png"), "Rider", IdsList);
        LoadItems(new Uri("avares://Menus/Assets/Entornos/NetBeans.png"), "NetBeans", IdsList);
        LoadItems(new Uri("avares://Menus/Assets/Entornos/PyCharm.png"), "PyCharm", IdsList);
        LoadItems(new Uri("avares://Menus/Assets/Entornos/Visual_Studio.png"), "Visual Studio", IdsList);
        LoadItems(new Uri("avares://Menus/Assets/Entornos/Visual_Studio_Code.png"), "Visual Studio Code", IdsList);
    }
    
    private void LoadSelect(ObservableCollection<LanguageModel> select)
    {
        
    }

    private void CargarLenguajeSelect()
    {
        
    }
}