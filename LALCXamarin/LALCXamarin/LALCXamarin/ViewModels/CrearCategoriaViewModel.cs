using LALC_UWP.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using LALCXamarin.Views;
using System.Net.Http;
using Newtonsoft.Json;

namespace LALCXamarin.ViewModels
{
    public class CrearCategoriaViewModel : Categoria
    {
        public string categorias_url = "https://10.0.2.2:44318/API/Categorias";
        public Command CrearCategoria { get; }

        public String Title {get;set;}
        public CrearCategoriaViewModel()
        {
            Title = "Crear categoría";

            //CrearCategoria = new Command(onCrearCategoria);
        }

        public async void OnCrearCategoria(Categoria ct)
        {

            var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            var client = new HttpClient(httpHandler);
            var serializedCategoria = JsonConvert.SerializeObject(ct);
            var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(categorias_url, dato);

            if (httpResponse.Content != null)
            {
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }
        }
    }
}
