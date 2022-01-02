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
using Windows.UI.Notifications;
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
                subcategoria = resultado;
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
            var tempConcepto = ((FrameworkElement)e.OriginalSource).DataContext as Concepto;
            if (tempConcepto != null)
            {
                conceptosMenuFlyout.ShowAt(conceptos, e.GetPosition(conceptos));
                tappedConcepto = tempConcepto.ConceptoID;
            } 
        }


        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("¿Está seguro de eliminar el concepto " + subcategoria.Conceptos.Where<Concepto>(p=>p.ConceptoID==tappedConcepto).FirstOrDefault().Titulo+" ?");
            dialog.Title = "Eliminar";
            dialog.Commands.Add(new UICommand("Si", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex =1;
            var cmd = await dialog.ShowAsync();

            if (cmd.Label == "Si")
            {
                var httpHandler = new HttpClientHandler();
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(conceptos_url + "/" + tappedConcepto);
                request.Method = HttpMethod.Delete;
                request.Headers.Add("Accept", "application/json");
                var client = new HttpClient(httpHandler);

                HttpResponseMessage response = await client.SendAsync(request);
                LoadConceptos();
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            EditarConcepto.conceptoSeleccionado = tappedConcepto;
            Frame.Navigate(typeof(EditarConcepto));
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CrearConcepto));
        }

        private async void Practicar_Click(object sender, RoutedEventArgs e)
        {
            if (subcategoria.Conceptos.Count() == 0)
            {
                //ShowToastNotification("No puedes practicar","Esta subcategoria aún no cuenta con conceptos");
                await new MessageDialog(subcategoria.Nombre+" aún no cuenta con conceptos", "No puedes practicar").ShowAsync();
               
            }
            else
            {
                Frame.Navigate(typeof(PracticaView));
            }
            
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

        private void ShowToastNotification(string title, string stringContent)
        {
            ToastNotifier ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(stringContent));
            Windows.Data.Xml.Dom.IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTime.Now.AddSeconds(4);
            ToastNotifier.Show(toast);
        }
    }

}
