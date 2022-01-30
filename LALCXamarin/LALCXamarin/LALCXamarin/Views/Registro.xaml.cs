using LALC_UWP.Models;
using LALCXamarin.Services;
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
        LalcAPI a;
        

        public Registro()
        {
            InitializeComponent();
            a = new LalcAPI();
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

        private async void Button_ClickedR(object sender, EventArgs e)
        {
            if (!comprobacionNom() && !comprobacionEma() && !comprobacionCon())
            {
                Usuario nuevoUsuario = new Usuario
                {
                    nombre = campoNombreR.Text,
                    email = campoEmailR.Text,
                    password = campoContraseñaR.Text
                };
                var usu = a.CrearUsuario(nuevoUsuario);
                if(usu != null)
                {
                    Usuario usuario = await a.getUsuarioActual();
                    App.actualUserId = usuario.UsuarioID;
                    /*Navigation.InsertPageBefore(new AppShell(), this);
                    await Navigation.PopAsync();*/
                    await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
                }
                else
                {
                    msRegistro.Text = "Error de registro";
                }
                 
            }
        }

        private async void irLogin(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}