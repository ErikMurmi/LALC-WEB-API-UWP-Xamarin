using LALC_UWP.Models;
using LALCXamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views.Subcategorias
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Subcategorias : ContentPage
    {
        LalcAPI lalc;
        public static Categoria cate = new Categoria();
        public List<Subcategoria> Elemen { get; set; }
        public Subcategorias()
        {
            InitializeComponent();
            lalc = new LalcAPI();
            Elemen = new List<Subcategoria>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            cate = await lalc.GetCategoria(cate.CategoriaID);
            Elemen = (List<Subcategoria>)cate.Subcategorias;
            SubcategoriasVista.ItemsSource = Elemen;
        }

        private async void SubcategoriasVista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Conceptos.Conceptos.sub = (Subcategoria)e.Item;
            await Navigation.PushAsync(new Conceptos.Conceptos());
        }

        private async void EliminarSubcategoria_Invoked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Eliminar", "¿Está seguro de eliminar la subcategoria?", "Si", "No");
            if (answer)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                if(await lalc.EliminarSubcategoria(id))
                {                  
                    this.OnAppearing();
                }
            }
        }

        private async void ModificarSubcategoria_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            int pas = Int16.Parse(id);
            EditarSubcategoria.subcategoriaSeleccionada = pas;
            await Navigation.PushAsync(new EditarSubcategoria());
        }
    }
}