using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
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
    public sealed partial class HistorialPracticas : Page
    {

        public string practicas_url = "https://localhost:44318/API/Practicas";
        public string practicas_url1 = "https://localhost:44318/API/Practicas/2";
        public HistorialPracticas()
        {
            this.InitializeComponent();
            LoadPracticas();
        }

        /*public  void LoadPracticas()
        {
            ListaPracticas.ItemsSource = MainPage.usuarioActual.Practicas;
        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        public async void LoadPracticas()
        {
            var praticas = new List<Practica>();
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(practicas_url);

            JsonArray jsonArray = JsonArray.Parse(response);

            foreach (var jsonRow in jsonArray)
            {
                JsonObject jsonObject = jsonRow.GetObject();
                var data = JsonConvert.DeserializeObject<Practica>(jsonObject.ToString());
                /*
                string id = jsonObject["PracticaID"].ToString();
                string subcatageoriaId = jsonObject["SubcategoriaID"].ToString();
                string cantidadConceptos = jsonObject["CantidadConceptos"].ToString();
                //string fecha = jsonObject["Fecha"].ToString();
                praticas.Add(new Practica { 
                    SubcategoriaID= Int32.Parse(subcatageoriaId),
                    CantidadConceptos = Int32.Parse(cantidadConceptos),
                    Fecha = DateTime.Now
                });*/
                praticas.Add(data);
            }

            ListaPracticas.ItemsSource = praticas;

        }

        /*public async void LoadPracticas()
        {
            //ListaPracticas.ItemsSource = MainPage.usuarioActual.Practicas;
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(practicas_url);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var praticas = new List<Practica>();
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Practica>(content);
                /*praticas.Add(data);
                httpHandler = new HttpClientHandler();
                request = new HttpRequestMessage();
                request.RequestUri = new Uri(practicas_url1);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                client = new HttpClient(httpHandler);
                response = await client.SendAsync(request);
                content = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<Practica>(content);
                praticas.Add(data);*/
        //var data = JsonConvert.DeserializeObject<List<Practica>>(content);

        /*foreach(var p in data)
        {
            if(p.UsuarioID == MainPage.actualUserId)
            {
                praticas.Add(p);
            }
        }

        ListaPracticas.ItemsSource = praticas;
        //ListaPracticas.ItemsSource = data;
    }
}*/
    }
}
