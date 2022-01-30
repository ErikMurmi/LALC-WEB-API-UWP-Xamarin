using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Security;
using System.Net;
using Newtonsoft.Json;
using LALC_UWP.Models;
using LALCXamarin.ViewModels.Categorias;
using LALCXamarin.Views.Subcategorias;

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarSubcategoria : ContentPage
    {
        public static int subcategoriaSeleccionada=1;
        public static Subcategoria seleccionada;
        public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, Boolean> ServerCertificateCustomValidationCallback { get; set; }

        public EditarSubcategoriaViewModel _viewModel;
        public EditarSubcategoria()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditarSubcategoriaViewModel();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            seleccionada =  await _viewModel.OnAppearing(subcategoriaSeleccionada);
            cargarSubcategoriaInfo();
        }

        public void cargarSubcategoriaInfo()
        {
            //Nombre.Text = "Editar " + seleccionada.Nombre;
            Nombre.Text = seleccionada.Nombre;
            if (seleccionada.Descripcion != null)
            {
                Descripcion.Text = seleccionada.Descripcion;
            }
            ColorPick.ViewModel.Color = Color.FromHex(seleccionada.Color);
            ColorPick.ViewModel.Hex = seleccionada.Color;
        }


        private async void GuardarEdit(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Nombre.Text))
            {
                await DisplayAlert("Nombre vacío", "La subcategoría debe tener un nombre", "OK");
            }
            else
            {
                bool answer = await DisplayAlert("Editar", "¿Está seguro de guardar los cambios realizados?", "Si", "No");

                if (answer)
                {
                    var subcategoriaEditada = new Subcategoria
                    {
                        Nombre = Nombre.Text,
                        SubcategoriaID = subcategoriaSeleccionada,
                        CategoriaID = seleccionada.CategoriaID,
                        Descripcion = Descripcion.Text,
                        Color = "#" + ColorPick.ViewModel.Hex.ToString()
                    };
                    if (await _viewModel.EditarSubcategoria(subcategoriaSeleccionada, subcategoriaEditada))
                    {
                        await Shell.Current.GoToAsync($"//{nameof(SubcategoriasView)}");
                    }
                }
            }
        }

        private async void CancelarEditar(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Salir sin guardar", "¿Está seguro de salir sin guardar los cambios", "Si", "No");

            if (answer)
            {
                await Navigation.PopAsync();
            }
        }
    }
}