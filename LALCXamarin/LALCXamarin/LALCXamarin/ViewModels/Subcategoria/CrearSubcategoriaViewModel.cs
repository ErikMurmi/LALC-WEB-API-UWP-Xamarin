using LALC_UWP.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using LALCXamarin.Views;
using LALCXamarin.Services;
using LALCXamarin.Views.Subcategorias;

namespace LALCXamarin.ViewModels
{
    public class CrearSubcategoriaViewModel : Categoria
    {

        public LalcAPI lalcAPI { get; set; }
        public String Title { get; set; }
        public CrearSubcategoriaViewModel()
        {
            Title = "Crear subcategoría";
            lalcAPI = new LalcAPI();
        }

        public async void OnCrearSubcategoria(Subcategoria NSubCategoria)
        {
            var creada = await lalcAPI.CrearSubcategoria(NSubCategoria);
            if (creada)
            {
                await Shell.Current.Navigation.PopAsync();
            }

        }

    }
}
