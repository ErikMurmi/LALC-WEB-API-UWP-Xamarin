using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCategoria : ContentPage
    {
        public CrearCategoria()
        {
            InitializeComponent();
        }
       

        private void CrearNuevaCategoria(object sender, EventArgs e)
        {
           // var rendererAssemblies = ColorPicker.UWP.ColorPickerEffects.GetRendererAssemblies();
            //Xamarin.Forms.Forms.Init(e, rendererAssemblies);
        }
    }
}