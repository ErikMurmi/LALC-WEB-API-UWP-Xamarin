using LALC_UWP.Models;
using LALCXamarin.Services;
using LALCXamarin.ViewModels.Practicas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views.Practicas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Practicas : ContentPage
    {
        public List<Practica> Items { get; set; }

        PracticaViewModel _viewModel;
        public Practicas()
        {
            InitializeComponent();
            Items = new List<Practica>();
            BindingContext = _viewModel = new PracticaViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Items = await _viewModel.OnAppearing();
            MyListView.ItemsSource = Items;
        }

       

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
