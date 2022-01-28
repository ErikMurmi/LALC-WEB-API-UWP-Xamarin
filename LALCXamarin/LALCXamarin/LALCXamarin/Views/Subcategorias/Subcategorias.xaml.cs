using LALC_UWP.Models;
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
        public static Categoria cate = new Categoria();
        public List<Subcategoria> Elemen { get; set; }
        public Subcategorias()
        {
            InitializeComponent();         
            Elemen = new List<Subcategoria>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Elemen = (List<Subcategoria>)cate.Subcategorias;
            SubcategoriasVista.ItemsSource = Elemen;
        }

        private async void SubcategoriasVista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Conceptos.Conceptos.sub = (Subcategoria)e.Item;
            await Navigation.PushAsync(new Conceptos.Conceptos());
        }
    }
}