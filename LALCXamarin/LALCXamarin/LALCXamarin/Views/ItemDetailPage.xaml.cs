using LALCXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LALCXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}