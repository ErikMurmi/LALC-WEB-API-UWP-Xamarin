using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
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
    public sealed partial class Registro : Page
    {
        
        public string usuarios_url = "https://localhost:44318/API/Usuarios";
        public Registro()
        {
            this.InitializeComponent();
        }

        private async void BotonRegistro(object sender, RoutedEventArgs e)
        {
            if (!comprobacionNom() || !comprobacionEma() || !comprobacionCon())
            {
                var us = new Usuario
                {
                    nombre = NombreText.Text,
                    email = EmailText.Text,
                    password = ContraseñaText.Password
                };
                var httpHandler = new HttpClientHandler();
                var client = new HttpClient(httpHandler);
                var content = JsonConvert.SerializeObject(us);
                var dato = new StringContent(content, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync(usuarios_url, dato);


                if (httpResponse.Content != null)
                {
                    string con = await httpResponse.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<Usuario>(con);

                    MainPage.actualUserId = resultado.UsuarioID;
                    Frame.Navigate(typeof(MainPage));
                }
            }
        }

        private bool comprobacionNom()
        {
            if (NombreText.Text.Length < 2)
            {
                msNombre.Text = "Este campo no puede contener menos de 2 caracteres";
                return true;
            }
            return false;
        }
        private bool comprobacionEma()
        {
            if (string.IsNullOrEmpty(EmailText.Text))
            {
                msEmail.Text = "Este campo no puede estar vacío";
                return true;
            }
            else if (!Regex.IsMatch(EmailText.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                msEmail.Text = "Correo inválido";
                return true;
            }
            else
            {
                msEmail.Text = string.Empty;
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
                msContraseña.Text = "'" + ContraseñaText + "'" + " no es una contraseña válida.";
                return true;
            }
            else if (string.IsNullOrEmpty(ContraseñaText.ToString()) 
                || string.IsNullOrWhiteSpace(ContraseñaText.ToString()) 
                || ContraseñaText.Password.Length < 8)
            {
                msContraseña.Text = "La contraseña debe contener más de 8 caracteres y no debe contener espacios en blanco.";
                return true;
            }
            else
            {
                msContraseña.Text = string.Empty;
                return false;
            }
        }

        private void backRegistro_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
