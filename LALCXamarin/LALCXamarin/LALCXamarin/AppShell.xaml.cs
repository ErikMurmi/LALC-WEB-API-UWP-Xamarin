using LALCXamarin.ViewModels;
using LALCXamarin.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LALCXamarin
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            CurrentItem = CategoriasIt;
        }

       /* protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (App.actualUserId == 0)
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                CurrentItem = CategoriasIt;
            }
        }*/

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
