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
    public sealed partial class CrearCategoria : Page
    {
        
        public static int categoriaSeleccionada;
        public string categorias_url = "https://localhost:44318/API/Categorias";
        public Categoria seleccionada;
        public CrearCategoria()
        {
            this.InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void NuevoColor_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //pickerShow = !pickerShow;
            if(CreaColor.Visibility == Visibility.Visible)
            {
                CreaColor.Visibility = Visibility.Collapsed;
            }
            else
            {
                CreaColor.Visibility = Visibility.Visible;
            }
            
        }

        private void Colorpick_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            NuevoColor.Background = new SolidColorBrush(args.NewColor);
        }

        private async void Crear_Click(object sender, RoutedEventArgs e)
        {
            var categoriaCreada = new Categoria
            {
                Nombre = Nombrenueva.Text,
                UsuarioID = 1,
                Descripcion = Descripcionnueva.Text,
                esPrioritaria = (bool)Prioridadnueva.IsChecked,
                Color = CreaColor.Color.ToHex()
            };
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var serializedCategoria = JsonConvert.SerializeObject(categoriaCreada);
            var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(categorias_url +"/"+categoriaSeleccionada, dato);
            
            if(httpResponse.Content != null)
            {
                Frame.Navigate(typeof(MainPage));
            }
            
        }
    }
}
