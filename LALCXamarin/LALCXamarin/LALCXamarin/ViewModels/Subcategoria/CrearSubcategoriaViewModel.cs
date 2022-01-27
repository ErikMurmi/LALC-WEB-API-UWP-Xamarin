using LALC_UWP.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using LALCXamarin.Views;
using LALCXamarin.Services;

namespace LALCXamarin.ViewModels
{
    public class CrearSubcategoriaViewModel : Categoria
    {

        public LalcAPI lalcAPI { get; set; }
        public String Title { get; set; }
        public CrearSubcategoriaViewModel()
        {
            Title = "Crear subcategoría";
            lalcAPI = new LalcAPI();
        }

        public async void OnCrearSubcategoria(Subcategoria NSubCategoria)
        {
            var creada = await lalcAPI.CrearSubcategoria(NSubCategoria);
            if (creada)
            {
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }

        }

        /*public async void OnCrearCategoria(Categoria ct)
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
        }*/
    }
}
