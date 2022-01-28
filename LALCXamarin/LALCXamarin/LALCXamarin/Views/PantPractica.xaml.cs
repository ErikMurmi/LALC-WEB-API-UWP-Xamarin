using LALC_UWP.Models;
using LALCXamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantPractica : ContentPage
    {
        private List<Concepto> conceptosPractica;
        private static Random rng = new Random();
        public int contadorConceptos = 1;
        private int indexPractica = 0;
        LalcAPI a;


        public PantPractica()
        {
            InitializeComponent();
             a = new LalcAPI();
            generarListaPractica();
        }
        public async void generarListaPractica()
        {
            CarruselConceptos.IsSwipeEnabled = true;
            var sub = await a.GetSubcategoria(1);
            contadorConceptos = 1;
            conceptosPractica = sub.Conceptos.ToList();
            conceptosPractica = Shuffle<Concepto>(conceptosPractica);
            CarruselConceptos.ItemsSource = conceptosPractica;
        }

        private List<T> Shuffle<T>(List<T> list)
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

        private async void CarruselConceptos_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            contadorConceptos++;
            int pos = e.CurrentPosition;
            if(pos == conceptosPractica.Count - 1)
            {
                await DisplayAlert("Fin", "Haz finalizado la práctica", "OK");
                CarruselConceptos.IsSwipeEnabled = false;
            }
        }

        private async void GuardarPractica_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Guardar", "¿Desea guardar esta práctica?", "No", "Si");
            if (!answer)
            {
                var nuevaPractica = new Practica
                {
                    SubcategoriaID = 1,
                    CantidadConceptos = contadorConceptos,
                    Fecha = DateTime.UtcNow
                };
                var pra = a.CrearPractica(nuevaPractica);
                await Shell.Current.GoToAsync($"//{nameof(Practicas)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }
        }
    }
}