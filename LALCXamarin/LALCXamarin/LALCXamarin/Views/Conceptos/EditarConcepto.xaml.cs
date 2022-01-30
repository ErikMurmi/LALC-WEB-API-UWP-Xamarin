using LALC_UWP.Models;
using LALCXamarin.ViewModels.Categorias;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views.Conceptos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarConcepto : ContentPage
    {
        public string conceptos_url = "https://10.0.2.2:44318/API/Conceptoes";
        public static int ConceptoSeleccionado = 1;
        public static Concepto seleccionado;

        EditarConceptoViewModel _viewModel;

        public EditarConcepto()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditarConceptoViewModel();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            seleccionado = await _viewModel.OnAppearing(ConceptoSeleccionado);
            cargarConceptoInfo();
        }

        public void cargarConceptoInfo()
        {

            //Titulo.Text = "Editar " + seleccionado.Titulo;
            Titulo.Text = seleccionado.Titulo;
            if (seleccionado.Definicion != null)
            {
                Definicion.Text = seleccionado.Definicion;
             }
        }


        private async void Editar(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Titulo.Text))
            {
                await DisplayAlert("Título vacío", "El concepto debe tener un título", "OK");
            }
            else
            {
                bool answer = await DisplayAlert("Editar", "¿Está seguro de guardar los cambios realizados?", "Si", "No");

                if (answer)
                {
                    var conceptoEditado = new Concepto
                    {
                        ConceptoID = ConceptoSeleccionado,
                        Titulo = Titulo.Text,
                        SubcategoriaID = seleccionado.SubcategoriaID,
                        Definicion = Definicion.Text,
                    };
                    if (await _viewModel.EditarConcepto(ConceptoSeleccionado, conceptoEditado))
                    {
                        await Shell.Current.Navigation.PopAsync();
                    }
                }
            }
        }


        private async void Cancelar(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Salir sin guardar", "¿Está seguro de salir sin guardar los cambios", "Si", "No");

            if (answer)
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }
    }



}