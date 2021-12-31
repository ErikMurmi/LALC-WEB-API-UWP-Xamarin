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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace LALC_UWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public string usuarios_url = "https://localhost:44318/API/Usuarios";
        public Login()
        {
            this.InitializeComponent();
        }
        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            MensajeError.Text = "";
        }
        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MensajeError.Text = "";
        }
        async void getData()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(usuarios_url);
            var data = JsonConvert.DeserializeObject<List<Usuario>>(response);
            foreach(var i in data)
            {
                if (i.email.Equals(EmailText.Text) && i.password.Equals(ContraseñaText.Text))
                {
                    MainPage.actualUserId = i.UsuarioID;
                    Frame.Navigate(typeof(MainPage));
                }
            }
            
        }

        public async void getData2()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(usuarios_url);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient(httpHandler);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Usuario>>(content);
                /*if (data.Count() == 0) 
                {
                    //abcd.Text = "vacio";
                }*/
                foreach (var i in data)
                {
                    if (i.email.Equals(EmailText.Text) && i.password.Equals(ContraseñaText.Text))
                    {
                        MainPage.actualUserId = i.UsuarioID;
                        Frame.Navigate(typeof(MainPage));
                    }
                }
            }
        }

        private void BotonInicioSesion(object sender, RoutedEventArgs e)
        {
            abcd.Text = "lleno1";
            getData2();
            //Frame.Navigate(typeof(MainPage));
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registro));
        }
    }
}
