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
using Windows.UI.Popups;
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
    public sealed partial class SubcategoriasView : Page
    {
        public string subcategorias_url = "https://localhost:44318/API/Subcategorias";
        public string categorias_url = "https://localhost:44318/API/Categorias";
        public static Categoria categoria;
        public int tappedSubcategoria;
        public SubcategoriasView()
        {
            this.InitializeComponent();
            LoadSubcategorias();
        }

        public async void LoadSubcategorias()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(categorias_url+"/"+categoria.CategoriaID);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Categoria>(content);
                SubcategoriasGrid.ItemsSource = resultado.Subcategorias;
                TituloSubcategorias.Text = resultado.Nombre;
            }
        }

        public void Subcategorias_ItemClick(object sender  , ItemClickEventArgs e)
        {
            var sb = (Subcategoria)e.ClickedItem;
            ConceptosView.subcategoria = sb;
            if (sb!=null)
            {
                Frame.Navigate(typeof(ConceptosView));
            }
            
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("¿Está seguro de eliminar la subcategoria " + categoria.Subcategorias.Where<Subcategoria>(p => p.SubcategoriaID == tappedSubcategoria).FirstOrDefault().Nombre + " ?");
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
                request.RequestUri = new Uri(subcategorias_url + "/" + tappedSubcategoria);
                request.Method = HttpMethod.Delete;
                request.Headers.Add("Accept", "application/json");
                var client = new HttpClient(httpHandler);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    LoadSubcategorias();
                }
            }        
            /*var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(subcategorias_url + "/" + tappedSubcategoria);
            request.Method = HttpMethod.Delete;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                LoadSubcategorias();
            }*/
        }

        private  void Editar_Click(object sender, RoutedEventArgs e)
        {
            EditarSubcategoria.subcategoriaSeleccionada = tappedSubcategoria;
            Frame.Navigate(typeof(EditarSubcategoria));
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CrearSubcategoria));
        }

        private void Subcategories_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            AdaptiveGridView subcategorias = (AdaptiveGridView)sender;
            subcategoriasMenuFlyout.ShowAt(subcategorias, e.GetPosition(subcategorias));
            var tempCategoria = ((FrameworkElement)e.OriginalSource).DataContext as Subcategoria;
            tappedSubcategoria = tempCategoria.SubcategoriaID;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void BuscarSb_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var filteredList = (List<Subcategoria>)categoria.Subcategorias;
                filteredList = filteredList.FindAll(s => s.Nombre.ToLower().Contains(sender.Text.ToLower()));
                SubcategoriasGrid.ItemsSource = filteredList;
            }
        }
    }
}
