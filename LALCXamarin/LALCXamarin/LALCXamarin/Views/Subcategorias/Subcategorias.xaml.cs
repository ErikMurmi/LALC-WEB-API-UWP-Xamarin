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
        public static Categoria cate;
        public List<Subcategoria> Items { get; set; }
        public Subcategorias()
        {
            InitializeComponent();
            cate = new Categoria();
            Items = new List<Subcategoria>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Items = (List<Subcategoria>)cate.Subcategorias;
            SubcategoriasVista.ItemsSource = Items;
        }
    }
}