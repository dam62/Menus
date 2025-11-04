using System;
using System.Collections.ObjectModel;

namespace Menus.Models;

public class RegisterModel
{
    public ObservableCollection<LanguageModel> selectedLanguages {get; set;}
    public string nombre {get; set;}
    public string correo {get; set;}
    public DateTime fecha {get; set;}
    public string poblado {get; set;}
    
    public RegisterModel(){}
}