using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBM._app_objs._vms._home;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBM._pages._menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class emenu_page : ContentView
    {
        vmmenu vmmenu;
        public emenu_page()
        {
            InitializeComponent();
        }
        public async Task RenderMenu(List<vbm.objs.e_menu_obj> e_Menu_Obj)
        {
            await Task.Delay(500);
            vmmenu = new vmmenu();
            await Task.Run(() => { vmmenu.createitem(e_Menu_Obj); });
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmmenu;
            });
        }

        async void britem_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (vbm.objs.e_menu_obj)ctr.BindingContext;
                var detailpage = new VBM._pages._menu.detail_page();
                await Navigation.PushAsync(detailpage);
                detailpage.Render(cv);
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }

        }
    }
}