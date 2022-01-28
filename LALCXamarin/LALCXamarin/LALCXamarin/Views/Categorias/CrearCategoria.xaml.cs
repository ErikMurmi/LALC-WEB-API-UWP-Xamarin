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

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCategoria : ContentPage
    {
        CrearCategoriaViewModel _viewModel { get; set; }

        public CrearCategoria()
        {
            InitializeComponent();
            _viewModel = new CrearCategoriaViewModel();
            //BindingContext = _viewModel = new CrearCategoriaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        /*private async void CrearNuevaCategoria(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Nombre.Text))
            {
                await DisplayAlert("Nombre vacío", "La categoría debe tener un nombre", "OK");
            }
            else {
                bool answer = await DisplayAlert("Crear", "¿Está seguro de crear la categoría?", "Si", "No");
                if (answer)
                {
                    Categoria categoriaCreada = new Categoria
                    {
                        UsuarioID = 1,
                        Nombre = Nombre.Text,
                        Descripcion = Descripcion.Text,
                        esPrioritaria = EsPrioritaria.IsChecked,
                        Color = "#4287f5"
                    };
                    _viewModel.onCrearCategoria(categoriaCreada);
                    var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
                    var client = new HttpClient(httpHandler);
                    var serializedCategoria = JsonConvert.SerializeObject(categoriaCreada);
                    var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
                    var httpResponse = await client.PostAsync(categorias_url, dato);

                    if (httpResponse.Content != null)
                    {
                        await Navigation.PopAsync();
                    }
                }
            } 
        }*/
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
                        UsuarioID = 1,
                        Nombre = Nombre.Text,
                        Descripcion = Descripcion.Text,
                        esPrioritaria = EsPrioritaria.IsChecked,
                        Color = "#"+colorx.ViewModel.Hex.ToString()
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