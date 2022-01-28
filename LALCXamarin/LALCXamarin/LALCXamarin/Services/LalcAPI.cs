using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LALCXamarin.Services
{
    public class LalcAPI
    {

        private HttpClientHandler httpHandler;
        private HttpClient client;

        private const string categorias_url = "https://10.0.2.2:44318/API/Categorias";
        private const string subcategorias_url = "https://10.0.2.2:44318/API/Subcategorias";
        public string conceptos_url = "https://10.0.2.2:44318/API/Conceptoes";
        public string practicas_url = "https://10.0.2.2:44318/API/Practicas";
        public LalcAPI()
        {
            httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            client = new HttpClient(httpHandler);
        }

        public async Task<Categoria> GetCategoria(int id)
        {

            HttpResponseMessage httpResponse = await client.GetAsync($"{categorias_url}/{id}");
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Categoria>(content);
                if (resultado != null)
                {
                    return resultado;
                }
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }

        public async Task<Boolean> CrearCategoria(Categoria Ncategoria)
        {
            Ncategoria.UsuarioID = App.actualUserId;
            var serializedCategoria = JsonConvert.SerializeObject(Ncategoria);
            var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(categorias_url, dato);

            if (httpResponse.Content != null)
            {
                return true;
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }

        public async Task<Boolean> EditarCategoria(int id, Categoria categoriaEditada)
        {
            var serializedCategoria = JsonConvert.SerializeObject(categoriaEditada);
            var dato = new StringContent(serializedCategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(categorias_url + "/" + id, dato);

            if (httpResponse.Content != null)
            {
                return true;
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }

        public async Task<Subcategoria> GetSubcategoria(int id)
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"{subcategorias_url}/{id}");
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Subcategoria>(content);
                if (resultado != null)
                {
                    return resultado;
                }
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }

        public async Task<Boolean> CrearSubcategoria(Subcategoria NSubCategoria)
        {
            var serializedSubcategoria = JsonConvert.SerializeObject(NSubCategoria);
            var dato = new StringContent(serializedSubcategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(subcategorias_url, dato);

            if (httpResponse.Content != null)
            {
                return true;
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }

        public async Task<Boolean> EditarSubcategoria(int id, Subcategoria subcategoriaEditada)
        {
            var serializedSubcategoria = JsonConvert.SerializeObject(subcategoriaEditada);
            var dato = new StringContent(serializedSubcategoria, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(subcategorias_url + "/" + id, dato);

            if (httpResponse.Content != null)
            {
                return true;
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }


        public async Task<Concepto> GetConcepto(int id)
        {

            HttpResponseMessage httpResponse = await client.GetAsync($"{conceptos_url}/{id}");
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Concepto>(content);
                if (resultado != null)
                {
                    return resultado;
                }
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }

        public async Task<Boolean> CrearConcepto(Concepto Nconcepto)
        {
            var serializedConcepto = JsonConvert.SerializeObject(Nconcepto);
            var dato = new StringContent(serializedConcepto, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(conceptos_url, dato);


            if (httpResponse.Content != null)
            {
                return true;
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }



        public async Task<Boolean> EditarConcepto(int id, Concepto conceptoEditado)
        {
            var serializedConcepto = JsonConvert.SerializeObject(conceptoEditado);
            var dato = new StringContent(serializedConcepto, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(conceptos_url + "/" + id, dato);

            if (httpResponse.Content != null)
            {
                return true;
            }
            throw new Exception(httpResponse.ReasonPhrase);
        }

        public async Task<List<Practica>> GetPracticas()
        {

            HttpResponseMessage httpResponse = await client.GetAsync(practicas_url);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Practica>>(content);
                if (resultado != null)
                {
                    return resultado;
                }
            }
            throw new Exception(httpResponse.ReasonPhrase);

        }

        public async Task<Boolean> EliminarPractica(String id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{practicas_url}/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            throw new Exception(response.ReasonPhrase);
        }



    }
}
