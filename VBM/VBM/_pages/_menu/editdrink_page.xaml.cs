using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBM._app_objs._vms._detail;
using VBM._pages._menu;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBM._pages._menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class editdrink_page : ContentPage
    {
        vmdrink vmdrink { get; set; }
        detail_page detail_Page;
        public editdrink_page()
        {
            InitializeComponent();
        }
        public async Task Render(detail_page detail_Page)
        {
            this.detail_Page = detail_Page;
            vmdrink = new vmdrink();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmdrink;
                busyindicator.IsBusy = false;
            });
        }

        async void grSelectedDrink_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (drinkEme)ctr.BindingContext;
                if(detail_Page != null)
                {
                    detail_Page.selecteddrink(cv.drk);
                }
                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
    }
}