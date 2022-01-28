using LALC_UWP.Models;
using LALCXamarin.Services;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views
{
    public partial class Categorias : ContentPage
    {
        LalcAPI lalc; 
      
        public Categorias()
        {
            lalc = new LalcAPI();
        }

        public Categorias(NavigationPage MainPage)
        {
            InitializeComponent();
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Usuario usuarioac = await lalc.GetUsuario(App.actualUserId);
            VistaCategorias.ItemsSource = usuarioac.Categorias;
        }

    }
}