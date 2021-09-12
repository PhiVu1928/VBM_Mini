using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBM._pages._main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class start_page : ContentPage
    {
        public start_page()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            localdb._manager = new _app_objs._general.app_manager();
            Task.Run(() =>
            {
                localdb._manager._cached.get_cached_values();
                localdb._manager._tools.start_prepare_data();
            });
        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await bdstart.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var loginpage = new VBM._pages._main.login_page();
                    Navigation.PushAsync(loginpage);
                }); 
                await bdstart.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                await bdstart.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
    }
}