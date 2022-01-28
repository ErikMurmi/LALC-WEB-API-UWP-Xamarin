using LALC_UWP.Models;
using LALCXamarin.Services;
using LALCXamarin.ViewModels;
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
        //CategoriasViewModel _viewModel;
        public Categorias()
        {
            InitializeComponent();
            lalc = new LalcAPI();
            Items = new List<Categoria>();
            //BindingContext = _viewModel = new CategoriasViewModel();
        }

        /*public Categorias(NavigationPage MainPage)
        {
            
            lalc = new LalcAPI();
            Items = new List<Categoria>();
        }*/

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //_viewModel.OnAppearing();
            //CategoriasVista.ItemsSource = _viewModel.cts;
            Usuario usuarioac = await lalc.GetUsuario(App.actualUserId);
            Items = (List<Categoria>)usuarioac.Categorias;
            CategoriasVista.ItemsSource = Items;
        }

    }
}