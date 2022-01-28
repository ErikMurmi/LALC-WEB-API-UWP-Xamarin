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
        public CategoriasViewModel()
        {
            Title = "Categorías";
            lalcAPI = new LalcAPI();
        }

       /* public async  List<Categoria> OnAppearing()
        {
            var us = await lalcAPI.GetUsuario(App.actualUserId);
            return us.Categorias;
        }*/
    }
}
