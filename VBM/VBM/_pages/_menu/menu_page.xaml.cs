using Acr.UserDialogs;
using Syncfusion.XForms.Border;
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
    public partial class menu_page : ContentPage
    {
        vmmenu vmmenu { get; set; }
        public menu_page()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            vmmenu = new vmmenu();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmmenu;
                var mg = localdb._manager;
                var val = mg._varialbles;
                tabview.SelectionChanged += Handle_SelectionChanged;
                if (tabview.Items[tabview.SelectedIndex].Content == null)
                {
                    foreach (var item in val._menus)
                    {
                        foreach (var sub in item.lst_sub_menu)
                        {
                            if (sub.name_vn == "Bánh mì")
                            {
                                emenu_page lstemenu = new emenu_page();
                                lstemenu.RenderMenu(sub.lst_emes);
                                tabview.Items[tabview.SelectedIndex].Content = lstemenu;
                                break;
                            }
                        }
                    }
                }


            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            updateslcart();
        }
        private void Handle_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            var mg = localdb._manager;
            var val = mg._varialbles;
            if (tabview.Items[e.Index].Content == null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (var items in val._menus)
                    {
                        if (items.name_vn == e.Name)
                        {
                            foreach (var item in items.lst_sub_menu)
                            {
                                emenu_page lstemenu = new emenu_page();
                                lstemenu.RenderMenu(item.lst_emes);
                                tabview.Items[e.Index].Content = lstemenu;
                                break;
                            }
                        }
                        else
                        {
                            foreach (var sub in items.lst_sub_menu)
                            {
                                if (sub.name_vn == e.Name)
                                {
                                    emenu_page lstemenu = new emenu_page();
                                    lstemenu.RenderMenu(sub.lst_emes);
                                    tabview.Items[e.Index].Content = lstemenu;
                                    break;
                                }
                            }
                        }
                    }

                });

            }
        }

        public void updateslcart()
        {
            vmmenu.slcart = localdb._manager._varialbles._cart_pros.Sum(x => x.slg);
        }

        async void bdviewcart_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cartpage = new VBM._pages._cart.cart_page();
                await Navigation.PushAsync(cartpage);
                cartpage.Render();
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
    }
}