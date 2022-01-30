using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LALC_UWP.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Security;
using LALCXamarin.ViewModels;
using LALCXamarin.Services;

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCategoria : ContentPage
    {
        LalcAPI lalc;
        CrearCategoriaViewModel _viewModel { get; set; }
        public static int usuarioid;
        
        public CrearCategoria()
        {
            InitializeComponent();
            _viewModel = new CrearCategoriaViewModel();
            BindingContext = _viewModel = new CrearCategoriaViewModel();
            usuarioid = new int();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void CrearNuevaCategoria(object sender, EventArgs e)
        {
         
            if (String.IsNullOrEmpty(Nombre.Text))
            {
                await DisplayAlert("Nombre vacío", "La categoría debe tener un nombre", "OK");
            }
            else
            {
                bool answer = await DisplayAlert("Crear", "¿Está seguro de crear la categoría?", "Si", "No");
                if (answer)
                {
                    Categoria categoriaCreada = new Categoria
                    {
                        UsuarioID = usuarioid,
                        Nombre = Nombre.Text,
                        Descripcion = Descripcion.Text,
                        esPrioritaria = EsPrioritaria.IsChecked,
                        Color = "#"+ColorPick.ViewModel.Hex.ToString()
                    };
                    _viewModel.OnCrearCategoria(categoriaCreada);
                }
            }
        }

        private async void CancelarCrear(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Salir sin guardar", "¿Está seguro de salir sin crear la categoria?", "Si", "No");

            if (answer)
            {
                await Navigation.PopAsync();
            }
        }
    }
}