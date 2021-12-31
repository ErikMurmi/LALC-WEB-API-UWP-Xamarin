using LALC_UWP.Models;
using Microsoft.Toolkit.Uwp.Helpers;
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
    public sealed partial class EditarSubcategorias : Page
    {
        public Subcategoria seleccionada;
        public static int subcategoriaSeleccionada;
        public string subcategorias_url = "https://localhost:44318/API/Subcategorias";
        public EditarSubcategorias()
        {
            this.InitializeComponent();
            cargarSubcategoriaInfo();
        }


        public async void cargarSubcategoriaInfo()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(subcategorias_url + "/" + subcategoriaSeleccionada);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Subcategoria>(content);
                seleccionada = resultado;
                TituloEditSubcategoria.Text = "Editar " + seleccionada.Nombre;
                EditName.Text = resultado.Nombre;
                if (resultado.Descripcion != null)
                {
                    EditDescripcion.Text = resultado.Descripcion;
                }

                EditColor.Background = new SolidColorBrush(ColorHelper.ToColor(resultado.Color));
                Colorpick.Color = ColorHelper.ToColor(resultado.Color);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void EditColor_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //pickerShow = !pickerShow;
            if (Colorpick.Visibility == Visibility.Visible)
            {
                Colorpick.Visibility = Visibility.Collapsed;
            }
            else
            {
                Colorpick.Visibility = Visibility.Visible;
            }

        }

        private void Colorpick_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            EditColor.Background = new SolidColorBrush(args.NewColor);
        }

        private async void Editar_Click(object sender, RoutedEventArgs e)
        {
            var subcategoriaEditada = new Subcategoria
            {
                Nombre = EditName.Text,
                SubcategoriaID = seleccionada.SubcategoriaID,
                CategoriaID = seleccionada.CategoriaID,
                Descripcion = EditDescripcion.Text,
                Color = Colorpick.Color.ToHex()
            };
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var serializedCategoria = JsonConvert.SerializeObject(subcategoriaEditada);
            var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(subcategorias_url + "/" + subcategoriaSeleccionada, dato);

            if (httpResponse.Content != null)
            {
                Frame.Navigate(typeof(SubcategoriasView));
            }

        }
    }
}
