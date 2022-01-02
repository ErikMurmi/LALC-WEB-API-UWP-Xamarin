using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
        public async void getData()
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
                    if (i.email.Equals(EmailText.Text) && i.password.Equals(ContraseñaText.Password))
                    {
                        MainPage.actualUserId = i.UsuarioID;
                        Frame.Navigate(typeof(MainPage));
                    }
                }
            }
        }

        private bool comprobacionEma()
        {
            if (string.IsNullOrEmpty(EmailText.Text))
            {
                msEmailR.Text = "Este campo no puede estar vacío";
                return true;
            }
            else if (!Regex.IsMatch(EmailText.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                msEmailR.Text = "Correo inválido";
                return true;
            }
            else
            {
                msEmailR.Text = string.Empty;
                return false;
            }

        }
        private bool comprobacionCon()
        {
            if (ContraseñaText.Password == "Contraseña"
                || ContraseñaText.Password == "contraseña"
                || ContraseñaText.Password == "password"
                || ContraseñaText.Password == "Password")
            {
                msContraseñaR.Text = "'" + ContraseñaText + "'" + " no es una contraseña válida.";
                return true;
            }
            else if (string.IsNullOrEmpty(ContraseñaText.ToString())
                || string.IsNullOrWhiteSpace(ContraseñaText.ToString())
                || ContraseñaText.Password.Length < 8)
            {
                msContraseñaR.Text = "La contraseña debe contener más de 8 caracteres y no debe contener espacios en blanco.";
                return true;
            }
            else
            {
                msContraseñaR.Text = string.Empty;
                return false;
            }
        }
        private void BotonInicioSesion(object sender, RoutedEventArgs e)
        {
            if (!comprobacionEma() || !comprobacionCon())
            {
                getData();
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registro));
        }

        private void EmailText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
