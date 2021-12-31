using LALC_UWP.Models;
using LALC_UWP.Views;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Controls;
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

namespace LALC_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConceptosView : Page
    {
        public string subcategorias_url = "https://localhost:44318/API/Subcategorias";
        public string conceptos_url = "https://localhost:44318/API/Conceptoes";

        public static Subcategoria subcategoria;
        public int tappedConcepto;
        public static SolidColorBrush color;
        public ConceptosView()
        {
            this.InitializeComponent();
            LoadConceptos();
        }

        public async void LoadConceptos()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(subcategorias_url + "/" + subcategoria.SubcategoriaID);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Subcategoria>(content);
                color = new SolidColorBrush(ColorHelper.ToColor(resultado.Color));
                TituloConceptos.Text = resultado.Nombre;
                ConceptosGrid.ItemsSource = resultado.Conceptos;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Conceptos_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            AdaptiveGridView conceptos = (AdaptiveGridView)sender;
            conceptosMenuFlyout.ShowAt(conceptos, e.GetPosition(conceptos));
            var tempConcepto = ((FrameworkElement)e.OriginalSource).DataContext as Concepto;
            tappedConcepto = tempConcepto.ConceptoID;
        }


        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(conceptos_url + "/" + tappedConcepto);
            request.Method = HttpMethod.Delete;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                LoadConceptos();
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            EditarConcepto.conceptoSeleccionado = tappedConcepto;
            Frame.Navigate(typeof(EditarConcepto));
        }


        private void BuscarCn_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var filteredList = (List<Concepto>)subcategoria.Conceptos;
                filteredList = filteredList.FindAll(s => s.Titulo.ToLower().Contains(sender.Text.ToLower()));
                ConceptosGrid.ItemsSource = filteredList;
            }
        }
    }

}
