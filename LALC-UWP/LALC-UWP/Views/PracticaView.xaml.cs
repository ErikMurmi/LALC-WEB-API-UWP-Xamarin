using LALC_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
        
        private List<Concepto> conceptosPractica;
        private int indexPractica = 0;
        public int contadorConceptos = 1;
        private static Random rng = new Random();
        public string subcategorias_url = "https://localhost:44318/API/Subcategorias";
        public string praticas_url = "https://localhost:44318/API/Practicas";
        public PracticaView()
        {
            this.InitializeComponent();
            generarListaPractica();
        }


        public void generarListaPractica()
        {
            contadorConceptos = 1;
            indexPractica = 0;
            conceptosPractica = ConceptosView.subcategoria.Conceptos.ToList();
            conceptosPractica = Shuffle<Concepto>(conceptosPractica);
            TituloCn.Text = conceptosPractica.First<Concepto>().Titulo;
            DefinicionCn.Text = conceptosPractica.First<Concepto>().Definicion;
        }

        public List<T> Shuffle<T>( List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public async void guardarPractica()
        {
            var nuevaPractica = new Practica
            {
                SubcategoriaID = ConceptosView.subcategoria.SubcategoriaID,
                CantidadConceptos = contadorConceptos,
                //UsuarioID = MainPage.actualUserId,
                Fecha = DateTime.UtcNow
            };

            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var serializedPractica = JsonConvert.SerializeObject(nuevaPractica);
            var dato = new StringContent(serializedPractica, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(praticas_url , dato);

            if (httpResponse.Content != null)
            {
                contadorConceptos = 1;
                indexPractica = 0;
                Frame.Navigate(typeof(ConceptosView));
            }
        }

        private async void SiguienteConcepto_Click(object sender, RoutedEventArgs e)
        {
            
            if (indexPractica < conceptosPractica.Count() - 1)
            {
                contadorConceptos += 1;
                indexPractica += 1;
                cargarConcepto(conceptosPractica[indexPractica]);
            }
            else if(indexPractica<conceptosPractica.Count)
            {
                MessageDialog dialog = new MessageDialog("¿Desea salir?");
                dialog.Title = "Completaste todos los conceptos";
                dialog.Commands.Add(new UICommand("Si y guardar", null));
                dialog.Commands.Add(new UICommand("Repetir", null));
                dialog.Commands.Add(new UICommand("No", null));
                
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 2;
                var cmd = await dialog.ShowAsync();
                if (cmd.Label == "Si y guardar")
                {
                    guardarPractica();
                    //Frame.Navigate(typeof(ConceptosView));
                }
                else if (cmd.Label == "Repetir")
                {
                    generarListaPractica();
                }
            }
        }

        public void cargarConcepto(Concepto cn)
        {
            TituloCn.Text = cn.Titulo;
            DefinicionCn.Text = cn.Definicion;
        }


        private async void GuardarPractica(object sender, TappedRoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("¿Desea guardar la práctica antes de salir?");
            dialog.Title = "Guardar";
            dialog.Commands.Add(new UICommand("Si", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.Commands.Add(new UICommand("No salir", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 2;
            var cmd = await dialog.ShowAsync();

            if (cmd.Label == "Si")
            {
                guardarPractica();
                Frame.Navigate(typeof(ConceptosView));
            }else if (cmd.Label == "No")
            {
                Frame.Navigate(typeof(ConceptosView));
            }            
        }

        private async void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("¿Desea guardar la práctica antes de salir?");
            dialog.Title = "Guardar";
            dialog.Commands.Add(new UICommand("Si", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.Commands.Add(new UICommand("No salir", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 2;
            var cmd = await dialog.ShowAsync();

            if (cmd.Label == "Si")
            {
                guardarPractica();
                Frame.Navigate(typeof(ConceptosView));
            }
            else if (cmd.Label == "No")
            {
                Frame.Navigate(typeof(ConceptosView));
            }
        }

    }
}
