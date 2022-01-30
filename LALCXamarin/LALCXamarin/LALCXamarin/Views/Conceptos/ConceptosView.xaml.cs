using LALC_UWP.Models;
using LALCXamarin.Services;
using LALCXamarin.ViewModels.Conceptos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views.Conceptos
{
    public partial class ConceptosView : ContentPage
    {
        LalcAPI lalc;
        public static Subcategoria sub = new Subcategoria();
        public static string color;
        public List<Concepto> Con { get; set; }
        public ConceptosView()
        {
            InitializeComponent();
            Con = new List<Concepto>();
            lalc = new LalcAPI();   
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            sub = await lalc.GetSubcategoria(sub.SubcategoriaID);
            color = sub.Color;
            Con = (List<Concepto>)sub.Conceptos;
            Cargar();
        }

        private void Cargar()
        {
            if (Con != null)
            {
                ConceptosVista.ItemsSource = Con;
            }
            else
            {
                Button button = new Button
                {
                    Text = "Crea una Conceptos",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center  

                };
                button.Clicked += OnButtonClicked;
            }
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            CrearConcepto.subid = sub.SubcategoriaID;
            await Navigation.PushAsync(new CrearConcepto());
        }

        private async void EliminarConcepto_Invoked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Eliminar", "¿Está seguro de eliminar este concepto?", "Si", "No");
            if (answer)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                if (await lalc.EliminarConcepto(id))
                {                    
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

        private void Barrabus3_TextChanged(object sender, TextChangedEventArgs e)
        {           
            List<Concepto> lista = (List<Concepto>)sub.Conceptos;
            var searchresult = lista.FindAll(s => s.Titulo.ToLower().Contains(Barrabus3.Text.ToLower()));
            ConceptosVista.ItemsSource = searchresult;
        }

        private async void CrearConcepto_Clicked(object sender, EventArgs e)
        {
            CrearConcepto.subid = sub.SubcategoriaID;
            await Navigation.PushAsync(new CrearConcepto());
        }
    }
}