using LALC_UWP.Models;
using LALCXamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrioritariasCat : ContentPage
    {
        LalcAPI lalc;
        public static List<Categoria> ItemsPriocat = new List<Categoria>();
        public PrioritariasCat()
        {
            InitializeComponent();
            lalc = new LalcAPI();           
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();          
            CategoriasPrioVista.ItemsSource = ItemsPriocat;
        }

        private async void Barrabusprio_TextChanged(object sender, TextChangedEventArgs e)
        {           
            List<Categoria> lista = ItemsPriocat;
            var searchresult = lista.FindAll(s => s.Nombre.ToLower().Contains(Barrabusprio.Text.ToLower()));
            CategoriasPrioVista.ItemsSource = searchresult;
        }

        private async void CategoriasPrioVista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Subcategorias.Subcategorias.cate = (Categoria)e.Item;
            await Navigation.PushAsync(new Subcategorias.Subcategorias());
        }

        private async void ModificarPrioCat_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            int pas = Int16.Parse(id);
            EditarCategoria.categoriaSeleccionada = pas;
            await Navigation.PushAsync(new EditarCategoria());
        }

        private async void EliminarPrioCat_Invoked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Eliminar", "¿Está seguro de eliminar la categoria?", "Si", "No");
            if (answer)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                if (await lalc.EliminarCategoria(id))
                {
                    this.OnAppearing();
                }
            }
        }

        private async void CrearPrioCategoria_Clicked(object sender, EventArgs e)
        {
            Usuario usuarioact = await lalc.GetUsuario(App.actualUserId);
            CrearCategoria.usuarioid = usuarioact.UsuarioID;
            await Navigation.PushAsync(new CrearCategoria());
        }
    }
}