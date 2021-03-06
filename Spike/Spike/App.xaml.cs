using Spike.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spike
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDkwMDY0QDMxMzkyZTMyMmUzMEtSM0FQUjBjdmlTMUEvTWZMNVNINTlKVHFWcmVVYmRCUXZ3eHBsVXNuclE9");
            InitializeComponent();

            //MainPage = new MainPage();
            Page page = FreshMvvm.FreshPageModelResolver.ResolvePageModel<HomeViewModel>();
            NavigationPage nav = new FreshMvvm.FreshNavigationContainer(page) as NavigationPage;
            MainPage = nav;
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
