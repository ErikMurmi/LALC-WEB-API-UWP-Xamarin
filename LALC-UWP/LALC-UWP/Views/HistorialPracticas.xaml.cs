using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Practica>>(content);
                var praticas = new List<Practica>();
                foreach(var p in data)
                {
                    if(p.UsuarioID == MainPage.actualUserId)
                    {
                        praticas.Add(p);
                    }
                }
                ListaPracticas.ItemsSource = data;
                //ListaPracticas.ItemsSource = data;
            }
        }
    }
}
