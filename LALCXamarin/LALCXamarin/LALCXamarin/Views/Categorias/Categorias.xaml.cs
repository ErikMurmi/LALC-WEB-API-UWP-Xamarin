using LALC_UWP.Models;
using LALCXamarin.Services;
using LALCXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
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

       

        private async void CategoriasVista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Subcategorias.Subcategorias.cate = (Categoria)e.Item;
            await Shell.Current.GoToAsync($"//{nameof(Subcategorias)}");
        }
    }
}