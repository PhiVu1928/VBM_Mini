using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBM._app_objs._vms._cart;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBM._pages._cart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cart_page : ContentPage
    {
        vmcart vmcart { get; set; }
        public cart_page()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            vmcart = new vmcart();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmcart;
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vmcart.RenderCart();
            vmcart.Rendertongtien();
        }
        private void grdDecreaseCartsl(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            var cv = (cartitem)ctr.BindingContext;

            cv.sl--;
            cv.pros.slg--;

            vmcart.RenderCart();
            vmcart.Rendertongtien();

        }
        private void grdIncreaseCartsl(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            var cv = (cartitem)ctr.BindingContext;

            cv.sl++;
            cv.pros.slg++;

            vmcart.RenderCart();
            vmcart.Rendertongtien();

        }
        async void grEdit_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 150);

            var cv = (cartitem)ctr.BindingContext;
            var page = new VBM._pages._menu.detail_page();
            await Navigation.PushAsync(page);
            page.Render(cv.pros);

            await ctr.ScaleTo(1, 150);
            this.IsEnabled = true;
        }
    }
}