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


        private async void Crear_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(Nombrenueva.Text))
            {
                await new MessageDialog("La categoría debe tener un nombre", "Nombre vacío").ShowAsync();
            }
            else {
                MessageDialog dialog = new MessageDialog("¿Está seguro de crear la categoría?");
                dialog.Title = "Crear";
                dialog.Commands.Add(new UICommand("Si", null));
                dialog.Commands.Add(new UICommand("No", null));
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;
                var cmd = await dialog.ShowAsync();

                if (cmd.Label == "Si")
                {


                    var categoriaCreada = new Categoria
                    {
                        Nombre = Nombrenueva.Text,
                        UsuarioID = MainPage.actualUserId,
                        Descripcion = Descripcionnueva.Text,
                        esPrioritaria = (bool)Prioridadnueva.IsChecked,
                        Color = CreaColor.Color.ToHex()
                    };
                    var httpHandler = new HttpClientHandler();
                    var client = new HttpClient(httpHandler);
                    var serializedCategoria = JsonConvert.SerializeObject(categoriaCreada);
                    var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
                    var httpResponse = await client.PostAsync(categorias_url, dato);

                    if (httpResponse.Content != null)
                    {
                        Frame.Navigate(typeof(MainPage));
                    }
                }
            } 
        }

        private void CreaColor_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            NuevoColor.Background = new SolidColorBrush(args.NewColor);
        }

        private async void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("¿Está seguro de salir sin crear la categoria?");
            dialog.Title = "Eliminar";
            dialog.Commands.Add(new UICommand("Si", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            var cmd = await dialog.ShowAsync();

            if (cmd.Label == "Si")
            {
                Frame.Navigate(typeof(MainPage));
            }

        }
    }
}
