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
        LalcAPI a;


        public PantPractica()
        {
            InitializeComponent();
             a = new LalcAPI();
        }
        public async void generarListaPractica()
        {
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
    }
}