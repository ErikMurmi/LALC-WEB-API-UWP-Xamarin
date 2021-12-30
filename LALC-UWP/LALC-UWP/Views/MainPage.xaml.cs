using LALC_UWP.Models;
using LALC_UWP.Views;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace LALC_UWP
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public string actualUserId = "1";
        public string categorias_url = "https://localhost:44318/API/Categorias";
        public string usuarios_url = "https://localhost:44318/API/Usuarios";
        public Usuario usuarioActual;
        public int tappedCategoria;
        public MainPage()
        {
            this.InitializeComponent();
            loadUserInfo();
        }


        public async void loadUserInfo()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(usuarios_url+"/"+actualUserId);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Usuario>(content);
                usuarioActual = resultado;
                CategoriasGrid.ItemsSource = usuarioActual.Categorias;
                loadCategoriasPrioritarias();
            }
        }
        
        public void loadCategoriasPrioritarias()
        {
            var filteredList = (List<Categoria>)usuarioActual.Categorias;
            filteredList = filteredList.FindAll(s => s.esPrioritaria);
            PrioritariasList.ItemsSource = filteredList;
        }
        private void Cards_ItemClick(object sender, ItemClickEventArgs e)
        {
            SubcategoriasView.categoria = (Categoria)e.ClickedItem;
            Frame.Navigate(typeof(SubcategoriasView));
        }


        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var filteredList =(List<Categoria>) usuarioActual.Categorias;
                filteredList = filteredList.FindAll(s => s.Nombre.ToLower().Contains(sender.Text.ToLower()));
                CategoriasGrid.ItemsSource = filteredList;
            }
        }

        private void CategoriasGrid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            AdaptiveGridView categorias = (AdaptiveGridView)sender;
            categoriasMenuFlyout.ShowAt(categorias, e.GetPosition(categorias));
            var tempCategoria = ((FrameworkElement)e.OriginalSource).DataContext as Categoria;
            tappedCategoria = tempCategoria.CategoriaID;
            EditarCategoria.categoriaSeleccionada = tempCategoria.CategoriaID;
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditarCategoria));
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(categorias_url+"/"+tappedCategoria);
            request.Method = HttpMethod.Delete;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                loadUserInfo();
            }
        }

    }

}
