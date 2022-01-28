using LALC_UWP.Models;
using LALCXamarin.Services;
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
        LalcAPI lalc;
        public static Subcategoria sub = new Subcategoria();
        public List<Concepto> Con { get; set; }
        public Conceptos()
        {
            InitializeComponent();
            Con = new List<Concepto>();
            lalc = new LalcAPI();   
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Con = (List<Concepto>)sub.Conceptos;
            ConceptosVista.ItemsSource = Con;
        }

        private async void EliminarConcepto_Invoked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Eliminar", "¿Está seguro de eliminar este concepto?", "Si", "No");
            if (answer)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                if (await lalc.EliminarConcepto(id))
                {
                    await DisplayAlert("Alert", "Entra al IF", "OK");
                    this.OnAppearing();
                }
                
            }
        }

        private async void ModificarConcepto_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            int pas = Int16.Parse(id);
            EditarConcepto.ConceptoSeleccionado = pas;
            await Navigation.PushAsync(new EditarConcepto());
        }

        private async void IrPractica_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PantPractica());
        }
    }
}