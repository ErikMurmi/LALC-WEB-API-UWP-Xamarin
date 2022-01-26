using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views.Conceptos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarConcepto : ContentPage
    {
        public string conceptos_url = "https://10.0.2.2:44318/API/Conceptoes";
        public static int ConceptoSeleccionado = 1;
        public static Concepto seleccionado;
        public EditarConcepto()
        {
            InitializeComponent();
            cargarConceptoInfo();
        }


        public async void cargarConceptoInfo()
        {
            var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(conceptos_url + "/" + ConceptoSeleccionado);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Concepto>(content);
                seleccionado = resultado;
                //Titulo.Text = "Editar " + seleccionado.Titulo;
                Titulo.Text = resultado.Titulo;
                if (resultado.Definicion != null)
                {
                    Definicion.Text = resultado.Definicion;
                }
            }
        }


        private async void Editar(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Titulo.Text))
            {
                await DisplayAlert("Título vacío", "La categoría debe tener un título", "OK");
            }
            else
            {
                bool answer = await DisplayAlert("Editar", "¿Está seguro de guardar los cambios realizados?", "Si", "No");

                if (answer)
                {
                    var conceptoEditado = new Concepto
                    {
                        ConceptoID = ConceptoSeleccionado,
                        Titulo = Titulo.Text,
                        SubcategoriaID = seleccionado.SubcategoriaID,
                        Definicion = Definicion.Text,
                    };
                    var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
                    var client = new HttpClient(httpHandler);
                    var serializedConcepto = JsonConvert.SerializeObject(conceptoEditado);
                    var dato = new StringContent(serializedConcepto, Encoding.UTF8, "application/json");
                    var httpResponse = await client.PutAsync(conceptos_url + "/" + ConceptoSeleccionado, dato);

                    if (httpResponse.Content != null)
                    {
                        await Navigation.PopAsync();
                    }
                }
            }
        }


        private async void Cancelar(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Salir sin guardar", "¿Está seguro de salir sin guardar los cambios", "Si", "No");

            if (answer)
            {
                await Navigation.PopAsync();
            }
        }
    }



}