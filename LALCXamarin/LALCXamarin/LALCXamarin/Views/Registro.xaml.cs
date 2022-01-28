using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }

        public static string CancionesUrl = $"https://10.0.2.2:44318/api/usuarios";
        public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, Boolean>
            ServerCertificateCustomValidationCallback { get; set; }

        private bool comprobacionEma()
        {
            if (string.IsNullOrEmpty(campoEmailR.Text))
            {
                msEmailR.Text = "Este campo no puede estar vacío";
                return true;
            }
            if (!Regex.IsMatch(campoEmailR.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
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

        private bool comprobacionNom()
        {
            if (campoNombreR.Text.Length < 2)
            {
                msNombreR.Text = "Campo Inválido (min 2 caracteres)";
                return true;
            }
            else
            {
                msNombreR.Text = string.Empty;
                return false;
            }
        }

        private bool comprobacionCon()
        {
            if (campoContraseñaR.Text.ToUpper() == "CONTRASEÑA" || campoContraseñaR.Text.ToUpper() == "PASSWORD")
            {
                msContraseñaR.Text = "'" + campoContraseñaR + "'" + " no es una contraseña válida.";
                return true;
            }
            if (string.IsNullOrEmpty(campoContraseñaR.ToString())
                || string.IsNullOrWhiteSpace(campoContraseñaR.ToString())
                || campoContraseñaR.Text.Length < 8)
            {
                msContraseñaR.Text = "Campo inválido (min 8 caracteres)";
                return true;
            }
            else
            {
                msContraseñaR.Text = string.Empty;
                return false;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!comprobacionNom() && !comprobacionEma() && !comprobacionCon())
            {
                var us = new Usuario
                {
                    nombre = campoNombreR.Text,
                    email = campoEmailR.Text,
                    password = campoContraseñaR.Text
                };
                var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
                HttpClient client = new HttpClient(httpHandler);

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(CancionesUrl);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);
                    App.actualUserId = 
                }
            }
        }

        private async void getData()
        {
            var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            HttpClient client = new HttpClient(httpHandler);

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(CancionesUrl);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);
                foreach (var i in resultado)
                {
                    if (i.email.Equals(campoEmail.Text) && i.password.Equals(campoContraseña.Text))
                    {
                        App.actualUserId = i.UsuarioID;
                        await Navigation.PushAsync(new AppShell());
                    }
                }
                if (App.actualUserId == 0)
                {
                    msContraseña.Text = "Contraseña o email incorrecto";
                }
            }
        }
    }
}