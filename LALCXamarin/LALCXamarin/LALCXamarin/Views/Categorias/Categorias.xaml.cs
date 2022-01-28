using LALC_UWP.Models;
using LALCXamarin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views
{
    public partial class Categorias : ContentPage
    {
        LalcAPI lalc;
        public List<Categoria> Items { get; set; }
        public Categorias()
        {
            
        }

        public Categorias(NavigationPage MainPage)
        {
            InitializeComponent();
            lalc = new LalcAPI();
            Items = new List<Categoria>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Usuario usuarioac = await lalc.GetUsuario(App.actualUserId);
            Items = (List<Categoria>)usuarioac.Categorias;
            CategoriasVista.ItemsSource = Items;
        }

    }
}