using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LALC_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditarConcepto : Page
    {

        public string conceptos_url = "https://localhost:44318/API/Conceptoes";
        public static int conceptoSeleccionado;
        public Concepto seleccionado;
        public EditarConcepto()
        {
            this.InitializeComponent();
            cargarConceptoInfo();
        }


        public async void cargarConceptoInfo()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(conceptos_url + "/" + conceptoSeleccionado);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Concepto>(content);
                seleccionado = resultado;
                TituloEditConcepto.Text = "Editar " + seleccionado.Titulo;
                EditTitulo.Text = resultado.Titulo;
                if (resultado.Definicion != null)
                {
                    EditDefinicion.Text = resultado.Definicion;
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }



        private async void Editar_Click(object sender, RoutedEventArgs e)
        {
            var conceptoEditado = new Concepto
            {
                ConceptoID = conceptoSeleccionado,
                Titulo = EditTitulo.Text,
                SubcategoriaID = seleccionado.SubcategoriaID,
                Definicion = EditDefinicion.Text,
            };
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var serializedConcepto = JsonConvert.SerializeObject(conceptoEditado);
            var dato = new StringContent(serializedConcepto, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(conceptos_url + "/" + conceptoSeleccionado, dato);

            if (httpResponse.Content != null)
            {
                Frame.Navigate(typeof(ConceptosView));
            }

        }
    }
}
