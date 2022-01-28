using LALC_UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views.Conceptos
{
    public partial class Conceptos : ContentPage
    {
        public static Subcategoria sub = new Subcategoria();
        public List<Concepto> Con { get; set; }
        public Conceptos()
        {
            InitializeComponent();
            Con = new List<Concepto>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Con = (List<Concepto>)sub.Conceptos;
            ConceptosVista.ItemsSource = Con;
        }
    }
}