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

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarCategoria : ContentPage
    {
        public static int categoriaSeleccionada=1;
        public static Categoria seleccionada;

        public EditarCategoriaViewModel _viewModel;
        public EditarCategoria()
        {
            InitializeComponent();
            //cargarCategoriaInfo();
            BindingContext = _viewModel = new EditarCategoriaViewModel();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            seleccionada =  await _viewModel.OnAppearing(categoriaSeleccionada);
            cargarCategoriaInfo();
        }

        public void cargarCategoriaInfo()
        {
            //Nombre.Text = "Editar " + seleccionada.Nombre;
            Nombre.Text = seleccionada.Nombre;
            if (seleccionada.Descripcion != null)
            {
                Descripcion.Text = seleccionada.Descripcion;
            }

            EsPrioritaria.IsChecked = seleccionada.esPrioritaria;
            ColorPick.ViewModel.Color = Color.FromHex(seleccionada.Color);
            ColorPick.ViewModel.Hex = seleccionada.Color;
                
        }


        private async void GuardarEdit(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Nombre.Text))
            {
                await DisplayAlert("Nombre vacío", "La categoría debe tener un nombre", "OK");
            }
            else
            {
                bool answer = await DisplayAlert("Editar", "¿Está seguro de guardar los cambios realizados?", "Si", "No");

                if (answer)
                {
                    var categoriaEditada = new Categoria
                    {
                        CategoriaID = categoriaSeleccionada,
                        UsuarioID = App.actualUserId,
                        Nombre = Nombre.Text,
                        Descripcion = Descripcion.Text,
                        esPrioritaria = EsPrioritaria.IsChecked,
                        Color = "#4287f5"
                    };
                    if (await _viewModel.EditarCategoria(categoriaSeleccionada, categoriaEditada))
                    {
                        await Navigation.PopAsync();
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