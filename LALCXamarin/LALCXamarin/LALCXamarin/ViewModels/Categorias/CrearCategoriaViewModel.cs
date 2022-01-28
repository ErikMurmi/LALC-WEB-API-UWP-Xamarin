using LALC_UWP.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using LALCXamarin.Views;
using LALCXamarin.Services;

namespace LALCXamarin.ViewModels
{
    public class CrearCategoriaViewModel : Categoria
    {

        public LalcAPI lalcAPI { get; set; }
        public String Title {get;set;}
        public CrearCategoriaViewModel()
        {
            Title = "Crear categoría";
            lalcAPI = new LalcAPI();
        }

        public async void OnCrearCategoria(Categoria ct)
        {
            var creada = await lalcAPI.CrearCategoria(ct);
            if (creada)
            {
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }
            
        }
    }
}
