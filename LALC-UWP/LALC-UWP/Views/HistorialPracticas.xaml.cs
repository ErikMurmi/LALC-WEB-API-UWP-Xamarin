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
using Windows.UI.Popups;
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
        public string subcategorias_url = "https://localhost:44318/API/Subcategorias";
        public int tappedPractica;
        public HistorialPracticas()
        {
            this.InitializeComponent();
            LoadPracticas();
        }


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
                string sb = await client.GetStringAsync(subcategorias_url + "/" + data.SubcategoriaID);
                var sb_dt = JsonConvert.DeserializeObject<Subcategoria>(sb);
                if(sb_dt.Categoria.UsuarioID == MainPage.actualUserId)
                {
                    praticas.Add(data);
                }
            }
             ListaPracticas.ItemsSource = praticas;
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("¿Está seguro de eliminar la práctica?");
            dialog.Title = "Eliminar";
            dialog.Commands.Add(new UICommand("Si", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            var cmd = await dialog.ShowAsync();

            if (cmd.Label == "Si")
            {
                var httpHandler = new HttpClientHandler();
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(practicas_url + "/" + tappedPractica);
                request.Method = HttpMethod.Delete;
                request.Headers.Add("Accept", "application/json");
                var client = new HttpClient(httpHandler);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    LoadPracticas();
                }
            }

        }

        private void ListaPracticas_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView categorias = (ListView)sender;
            var tempPractica = ((FrameworkElement)e.OriginalSource).DataContext as Practica;
            if (tempPractica!=null)
            {
                practicasMenuFlyout.ShowAt(categorias, e.GetPosition(categorias));
                tappedPractica = tempPractica.PracticaID;
            }   
        }

    }
}
