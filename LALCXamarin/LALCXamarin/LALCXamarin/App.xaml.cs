using LALC_UWP.Models;
using LALCXamarin.Services;
using LALCXamarin.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LALCXamarin
{
    public partial class App : Application
    {
        public static int actualUserId = 1;
        public static Usuario usuarioActual;
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
