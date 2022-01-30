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
            await Navigation.PushAsync(new Subcategorias.Subcategorias());
        }

        private async void EliminarCategoria_Invoked(object sender, EventArgs e)
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

        private async void ModificarCategoria_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            int pas = Int16.Parse(id);
            EditarCategoria.categoriaSeleccionada = pas;
            await Navigation.PushAsync(new EditarCategoria());
        }

        private async void BarraBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            Usuario usuarioact = await lalc.GetUsuario(App.actualUserId);           
            List<Categoria> lista = (List<Categoria>)usuarioact.Categorias;
            var searchresult = lista.FindAll(s => s.Nombre.ToLower().Contains(Barrabus.Text.ToLower()));
            CategoriasVista.ItemsSource = searchresult;
        }

        private async void CrearCategoria_Clicked(object sender, EventArgs e)
        {
            Usuario usuarioact = await lalc.GetUsuario(App.actualUserId);
            CrearCategoria.usuarioid = usuarioact.UsuarioID;
            await Navigation.PushAsync(new CrearCategoria());
        }
    }
}