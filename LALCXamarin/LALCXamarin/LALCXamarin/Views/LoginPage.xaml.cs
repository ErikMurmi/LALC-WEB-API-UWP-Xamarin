using LALC_UWP.Models;
using LALCXamarin.ViewModels;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        public static string CancionesUrl = $"https://10.0.2.2:44318/api/usuarios";
        public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, Boolean>
            ServerCertificateCustomValidationCallback{ get; set; }

        private bool comprobacionEma()
        {
            if (string.IsNullOrEmpty(campoEmail.Text))
            {
                msEmail.Text = "Este campo no puede estar vacío";
                return true;
            }
            if (!Regex.IsMatch(campoEmail.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
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
            if (string.IsNullOrEmpty(campoContraseña.ToString())
                || string.IsNullOrWhiteSpace(campoContraseña.ToString())
                || campoContraseña.Text.Length < 8)
            {
                msContraseña.Text = "Campo inválido (min 8 caracteres)";
                return true;
            }
            if (campoContraseña.Text.ToUpper() == "CONTRASEÑA" || campoContraseña.Text.ToUpper() == "PASSWORD")
            {
                msContraseña.Text = "'" + campoContraseña + "'" + " no es una contraseña válida.";
                return true;
            }
            else
            {
                msContraseña.Text = string.Empty;
                return false;
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
                        await Navigation.PushAsync(new Categorias());
                    }
                }
                if (App.actualUserId == 0)
                {
                    msContraseña.Text = "Contraseña o email incorrecto";
                }
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!comprobacionEma() && !comprobacionCon())
            {
                getData();
            }
        }
        private async Task IreRegistro_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }
    }
}