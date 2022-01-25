using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Security;
using System.Net;
using Newtonsoft.Json;
using LALC_UWP.Models;

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarCategoria : ContentPage
    {
        public string categorias_url = "https://10.0.2.2:44318/API/Categorias";
        public static int categoriaSeleccionada=1;
        public static Categoria seleccionada;
        public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, Boolean> ServerCertificateCustomValidationCallback { get; set; }


        public EditarCategoria()
        {
            InitializeComponent();
            cargarCategoriaInfo();
        }

        public async void cargarCategoriaInfo()
        {
            var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(categorias_url + "/" + categoriaSeleccionada);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Categoria>(content);
                seleccionada = resultado;
                Nombre.Text = "Editar " + seleccionada.Nombre;
                Nombre.Text = resultado.Nombre;
                if (resultado.Descripcion != null)
                {
                    Descripcion.Text = resultado.Descripcion;
                }

                EsPrioritaria.IsChecked = resultado.esPrioritaria;
                //EditColor.Background = new SolidColorBrush(ColorHelper.ToColor(resultado.Color));
                //Colorpick.Color = ColorHelper.ToColor(resultado.Color);
            }
        }




        private async void GuardarEdit(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Nombre.Text))
            {
                await DisplayAlert("Nombre vacío", "La categoría debe tener un nombre", "OK");
            }
            else
            {
                bool answer = await DisplayAlert("Editar", "¿Está seguro de guardar los cambios realizados?", "Si", "No");

                if (answer)
                {
                    var categoriaEditada = new Categoria
                    {
                        CategoriaID=1,
                        UsuarioID = 1,
                        Nombre = Nombre.Text,
                        Descripcion = Descripcion.Text,
                        esPrioritaria = EsPrioritaria.IsChecked,
                        Color = "#4287f5"

                    };
                    var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
                    var client = new HttpClient(httpHandler);
                    var serializedCategoria = JsonConvert.SerializeObject(categoriaEditada);
                    var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
                    var httpResponse = await client.PutAsync(categorias_url + "/" + categoriaSeleccionada, dato);

                    if (httpResponse.Content != null)
                    {
                        await Navigation.PopAsync();
                    }
                }
            }
        }


        private async void CancelarEditar(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Salir sin guardar", "¿Está seguro de salir sin guardar los cambios", "Si", "No");

            if (answer)
            {
                await Navigation.PopAsync();
            }
        }
    }
}