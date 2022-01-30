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
    public partial class SubcategoriasView : ContentPage
    {
        LalcAPI lalc;
        public static Categoria cate = new Categoria();
        public List<Subcategoria> Elemen { get; set; }
        
        public SubcategoriasView()
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


        private async void OnButtonClicked(object sender, EventArgs e)
        {
            CrearSubcategoria.categoriaid = cate.CategoriaID;
            await Navigation.PushAsync(new CrearSubcategoria());
        }

        private async void SubcategoriasVista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Conceptos.ConceptosView.sub = (Subcategoria)e.Item;           
            await Navigation.PushAsync(new Conceptos.ConceptosView());
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

        private void Barrabus2_TextChanged(object sender, TextChangedEventArgs e)
        {          
            List<Subcategoria> lista = (List<Subcategoria>)cate.Subcategorias;
            var searchresult = lista.FindAll(s => s.Nombre.ToLower().Contains(Barrabus2.Text.ToLower()));
            SubcategoriasVista.ItemsSource = searchresult;
        }

        private async void CrearSubcategoria_Clicked(object sender, EventArgs e)
        {
            CrearSubcategoria.categoriaid = cate.CategoriaID;
            await Navigation.PushAsync(new CrearSubcategoria());
        }
    }
}