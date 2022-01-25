using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LALC_UWP.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Security;

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCategoria : ContentPage
    {
        public string categorias_url = "https://10.0.2.2:44318/API/Categorias";
        public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, Boolean> ServerCertificateCustomValidationCallback { get; set; }

        public CrearCategoria()
        {
            InitializeComponent();
        }
       

        private async void CrearNuevaCategoria(object sender, EventArgs e)
        {
            /*Categoria nueva = new Categoria
            {
                UsuarioID = 0,
                Nombre = Nombre.Text,
                Descripcion = Descripcion.Text,
                esPrioritaria = EsPrioritaria.IsChecked,
                Color = "#4287f5"
            };*/
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