using Acr.UserDialogs;
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
    public partial class login_page : ContentPage
    {
        public login_page()
        {
            InitializeComponent();
        }


        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await bdnonlogin.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var menupage = new _pages._menu.menu_page();
                await Navigation.PushAsync(menupage);
                menupage.Render();
                await bdnonlogin.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("aaa", ex.ToString(), "OK");
                await bdnonlogin.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
    }
}