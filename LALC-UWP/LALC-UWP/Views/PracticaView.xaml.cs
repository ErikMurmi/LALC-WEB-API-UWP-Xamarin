using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class PracticaView : Page
    {
        
        public Subcategoria seleccionada;
        public static int subcategoriaSeleccionada;
        public int contadorConceptos;
        public string subcategorias_url = "https://localhost:44318/API/Subcategorias";
        public string praticas_url = "https://localhost:44318/API/Practicas";
        public PracticaView()
        {
            this.InitializeComponent();
        }

        public async void guardarPractica()
        {
            var nuevaPractica = new Practica
            {
                SubcategoriaID = seleccionada.SubcategoriaID,
                CantidadConceptos = contadorConceptos,
                Fecha = DateTime.UtcNow.Date
            };

            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var serializedCategoria = JsonConvert.SerializeObject(nuevaPractica);
            var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(praticas_url , dato);

            if (httpResponse.Content != null)
            {
                Frame.Navigate(typeof(MainPage));
            }
        }


    }
}
