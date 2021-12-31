using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class CrearConcepto : Page
    {
        public string conceptos_url = "https://localhost:44318/API/Conceptoes";
        public CrearConcepto()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void Crear_ClickCn(object sender, RoutedEventArgs e)
        {
            var conceptoCreado = new Concepto
            {
                Titulo = NombrenuevaCn.Text,
                SubcategoriaID = ConceptosView.subcategoria.SubcategoriaID,
                Definicion = DescripcionnuevaCn.Text
            };
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var serializedSubcategoria = JsonConvert.SerializeObject(conceptoCreado);
            var dato = new StringContent(serializedSubcategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(conceptos_url, dato);

            if (httpResponse.Content != null)
            {
                Frame.Navigate(typeof(ConceptosView));
            }

        }
    }
}
