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
    public class CategoriasViewModel : Categoria
    {

        public LalcAPI lalcAPI { get; set; }
        public String Title { get; set; }
        public List<Categoria> cts;
        public CategoriasViewModel()
        {
            Title = "Categorías";
            lalcAPI = new LalcAPI();
        }

        public async void OnAppearing()
        {
            var us = await lalcAPI.GetUsuario(App.actualUserId);
            cts = (List<Categoria>)us.Categorias;
        }
    }
}
