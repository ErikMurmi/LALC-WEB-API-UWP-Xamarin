using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.ViewModels.Conceptos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CrearConcepto : ContentPage
    {
        public string conceptos_url = "https://10.0.2.2:44318/API/Conceptoes";
        public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, Boolean> ServerCertificateCustomValidationCallback { get; set; }

        CrearConceptoViewModel _viewModel;
        
        public CrearConcepto()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CrearConceptoViewModel();
        }

        /*private async void CrearNuevoConcepto(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Titulo.Text))
            {
                await DisplayAlert("Nombre vacío", "El concepto debe tener un titulo", "OK");
            }
            else
            {
                bool answer = await DisplayAlert("Crear", "¿Está seguro de crear el concepto?", "Si", "No");
                if (answer)
                {
                    var conceptoCreado = new Concepto
                    {
                        Titulo = Titulo.Text,
                        SubcategoriaID = 1,
                        Definicion = Definicion.Text
                    };
                    var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
                    var client = new HttpClient(httpHandler);
                    var serializedSubcategoria = JsonConvert.SerializeObject(conceptoCreado);
                    var dato = new StringContent(serializedSubcategoria, Encoding.UTF8, "application/json");
                    var httpResponse = await client.PostAsync(conceptos_url, dato);

                    if (httpResponse.Content != null)
                    {
                        await Navigation.PopAsync();
                    }
                }
            }
        }*/

        private async void CrearNuevoConcepto(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Titulo.Text))
            {
                await DisplayAlert("Nombre vacío", "El concepto debe tener un titulo", "OK");
            }
            else
            {
                bool answer = await DisplayAlert("Crear", "¿Está seguro de crear el concepto?", "Si", "No");
                if (answer)
                {
                    var conceptoCreado = new Concepto
                    {
                        Titulo = Titulo.Text,
                        SubcategoriaID = 1,
                        Definicion = Definicion.Text
                    };
                    _viewModel.OnCrearConcepto(conceptoCreado);
                }
            }
        }

        private async void CancelarCrear(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Salir sin guardar", "¿Está seguro de salir sin crear la categoria?", "Si", "No");
            if(answer)
            {
                await Navigation.PopAsync();
            }
        }



    }
}