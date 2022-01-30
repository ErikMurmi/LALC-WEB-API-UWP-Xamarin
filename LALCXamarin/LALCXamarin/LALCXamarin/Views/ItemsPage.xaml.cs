using LALCXamarin.Models;
using LALCXamarin.ViewModels;
using LALCXamarin.ViewModels.Conceptos;
using LALCXamarin.Views;
using LALCXamarin.Views.Conceptos;
using LALCXamarin.Views.Subcategorias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearCategoria());
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarCategoria());
        }

        private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearConcepto());
        }

        private async void ToolbarItem_Clicked_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarConcepto());
        }

        private async void ToolbarItem_Clicked_4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearSubcategoria());
        }

        private async void ToolbarItem_Clicked_5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarSubcategoria());
        }

        private async void ToolbarItem_Clicked_6(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoriasView());
        }

        private async void ToolbarItem_Clicked_7(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PantPractica());
        }
    }
}