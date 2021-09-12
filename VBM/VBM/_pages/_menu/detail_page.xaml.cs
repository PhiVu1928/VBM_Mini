using Syncfusion.XForms.Border;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vbm;
using VBM._app_objs._vms._detail;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBM._pages._menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detail_page : ContentPage
    {
        vmdetail vmdetail { get; set; }
        vmcartedit vmcartedit { get; set; }
        public detail_page()
        {
            InitializeComponent();
        }
        public async Task Render(vbm.objs.e_menu_obj e_Menu_Obj)
        {
            vmdetail = new vmdetail(e_Menu_Obj);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmdetail;
                string url = "http://manage.vuabanhmi.com/SpImg/";
                Selecteditemimg.Source = url + vmdetail.SelectedItem.img;
                Selecteditemname.Text = vmdetail.SelectedItem.name_vn;
            });            
        }

        public async Task Render(VBM._app_objs._general.cart_pros cart_Pros)
        {
            vmcartedit = new vmcartedit(cart_Pros);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmcartedit;
                string url = "http://manage.vuabanhmi.com/SpImg/";
                Selecteditemimg.Source = url + vmcartedit.SelectedItem.img;
                Selecteditemname.Text = vmcartedit.SelectedItem.name_vn;
            });
        }    

        async void bdsize_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            var cv = (sizeStatus)ctr.BindingContext;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                if(vmdetail != null)
                {
                    vmdetail.Selecteditemid = cv.eme.id;
                    foreach (var items in vmdetail.Size_status)
                    {
                        if (cv.eme.id == items.eme.id)
                        {
                            items.Selected = true;
                        }
                        else
                        {
                            items.Selected = false;
                        }
                    }
                    vmdetail.RenderGia();
                }
                if(vmcartedit != null)
                {
                    vmcartedit.Selecteditemid = cv.eme.id;
                    foreach(var item in vmcartedit.Size_status)
                    {
                        if(cv.eme.id == item.eme.id)
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                    vmcartedit.RenderGia();
                }
                
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

        void spice_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            var cv = (spiceStatus)ctr.BindingContext;

            double x = 0;
            double y = 0;


            View view = null;
            if (vmdetail != null) view = collectionviewSpices.Children[vmdetail.Spices.IndexOf(cv)];
            if (vmcartedit != null) view = collectionviewSpices.Children[vmcartedit.Spices.IndexOf(cv)];
            x = tools.GetScreenCoordinates(view).X;
            y = tools.GetScreenCoordinates(view).Y - scrview.ScrollY - 10;

            if (vmdetail != null) vmdetail.createSpiceSelects(cv);
            if (vmcartedit != null) vmcartedit.createSpiceSelects(cv);

            popupSpice.Show(x, y);
        }
        void popupSpice_Opened(System.Object sender, System.EventArgs e)
        {
            popupSpice.IsVisible = true;
        }

        void popupSpice_Closed(System.Object sender, System.EventArgs e)
        {
            popupSpice.IsVisible = false;
        }
        private void btnSelectedSpice_clicked(object sender, EventArgs e)
        {
            var ctr = sender as SfButton;
            var cv = (spiceSelect)ctr.BindingContext;
            cv.spiStatus.idSelected = cv.item.id;
            cv.spiStatus.name = cv.item.name_vn;
            popupSpice.IsOpen = false;
        }

        async void bdselectdrink(object sender, EventArgs e)
        {
            await btndrink.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var drinkpage = new VBM._pages._menu.editdrink_page();
                await Navigation.PushAsync(drinkpage);
                drinkpage.Render(this);
                await btndrink.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                await btndrink.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
        public void selecteddrink(vbm.objs.e_menu_obj drink)
        {
            if(vmdetail != null)
            {
                if (vmdetail.DrinkSelected == null)
                {
                    vmdetail.DrinkSelected = new DrinkSelected();
                    vmdetail.vsbdrinkrecom = false;
                    vmdetail.vsbdrinkselected = true;
                }
                vmdetail.DrinkSelected.DrinkSelectedChange(drink);
                vmdetail.RenderGia();
            }
            if(vmcartedit != null)
            {
                if(vmcartedit.DrinkSelected == null)
                {
                    vmcartedit.DrinkSelected = new DrinkSelected();
                    vmcartedit.vsbdrinkrecom = false;
                    vmcartedit.vsbdrinkselected = true;
                }
                vmcartedit.DrinkSelected.DrinkSelectedChange(drink);
                vmcartedit.RenderGia();
            }
            
        }

        async void grdChangeDrink(object sender, EventArgs e)
        {
            var Drinkpage = new VBM._pages._menu.editdrink_page();
            await Navigation.PushAsync(Drinkpage);
            Drinkpage.Render(this);

        }
        private void grdDeleteDrink(object sender, EventArgs e)
        {
            if(vmdetail != null)
            {
                vmdetail.DrinkSelected = null;
                vmdetail.vsbdrinkrecom = true;
                vmdetail.vsbdrinkselected = false;
                vmdetail.RenderGia();
            }
            if(vmcartedit != null)
            {
                vmcartedit.DrinkSelected = null;
                vmcartedit.vsbdrinkrecom = true;
                vmcartedit.vsbdrinkselected = false;
                vmcartedit.RenderGia();
            }
        }

        private void grdDecreaseItemsl(object sender, EventArgs e)
        {
            if(vmdetail != null)
            {
                if (vmdetail.sl > 1)
                {
                    vmdetail.sl--;
                    vmdetail.RenderGia();
                }
            }
            if(vmcartedit != null)
            {
                if(vmcartedit.sl > 1)
                {
                    vmcartedit.sl--;
                    vmcartedit.RenderGia();
                }
            }
        }
        private void grdIncreateItemsl(object sender, EventArgs e)
        {
            if(vmdetail != null)
            {
                vmdetail.sl++;
                vmdetail.RenderGia();
            }
            if(vmcartedit != null)
            {
                vmcartedit.sl++;
                vmcartedit.RenderGia();
            }
        }

        private void grdDecreaseExtra(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            var cv = (extraStatus)ctr.BindingContext;
            if(cv.sl > 0)
            {
                cv.sl--;
            }
            if (vmdetail != null)
            {
                vmdetail.RenderGia();
            }
            if (vmcartedit != null)
            {
                vmcartedit.RenderGia();
            }
        }
        private void grdIncreaseExtra(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            var cv = (extraStatus)ctr.BindingContext;
            cv.sl++;
            if(vmdetail != null)
            {
                vmdetail.RenderGia();
            }
            if (vmcartedit != null)
            {
                vmcartedit.RenderGia();
            }
        }

        async void grdAddtocart(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var mg = localdb._manager;
                var val = mg._varialbles;
                if (vmdetail != null)
                {
                    var data = VBM._app_objs._general.cart_pros.createAddProd(vmdetail.Selecteditemid, vmdetail.DrinkSelected);
                    if (data != null)
                    {
                        data.slg = vmdetail.sl;
                        foreach (var t1 in vmdetail.extras)
                        {
                            if (t1.sl > 0)
                            {
                                data.extras.Add(new VBM._app_objs._general.cart_ex { price = t1.extra.price, id = t1.extra.id, name_vn = t1.extra.name_vn, sl = t1.sl });
                            }
                        }
                        foreach (var t1 in vmdetail.Spices)
                        {
                            foreach (var t2 in t1.spices.lst_items)
                            {
                                if (t2.size != 2 && t2.id == t1.idSelected)
                                {
                                    data.spices.Add(new VBM._app_objs._general.cart_spice { id = t2.id, name_vn =t2.name_vn, name_en = t2.name_eng });
                                }
                            }
                        }
                        val._cart_pros.Add(data);
                    }
                }       
                if(vmcartedit != null)
                {
                    var data = VBM._app_objs._general.cart_pros.createAddProd(vmcartedit.Selecteditemid, vmcartedit.DrinkSelected);
                    if(data != null)
                    {
                        data.slg = vmcartedit.sl;
                        if(vmcartedit.pros.orderType < 0)
                        {
                            data.orderType = vmcartedit.pros.orderType;
                            data.orderCode = vmcartedit.pros.orderCode;
                            data.dongia = vmcartedit.pros.dongia;
                            data.isGetLater = vmcartedit.pros.isGetLater;
                            data.isGetLaterOption = vmcartedit.pros.isGetLaterOption;
                            data.drinkid = vmcartedit.DrinkSelected.id;
                        }
                        foreach (var t1 in vmcartedit.extras)
                        {
                            if (t1.sl > 0)
                            {
                                data.extras.Add(new VBM._app_objs._general.cart_ex { price = t1.extra.price, id = t1.extra.id, name_vn = t1.extra.name_vn, sl = t1.sl });
                            }
                        }
                        foreach (var t1 in vmcartedit.Spices)
                        {
                            foreach (var t2 in t1.spices.lst_items)
                            {
                                if (t2.size != 2 && t2.id == t1.idSelected)
                                {
                                    data.spices.Add(new VBM._app_objs._general.cart_spice { id = t2.id, name_vn = t2.name_vn, name_en = t2.name_eng });
                                }
                            }
                        }
                        val._cart_pros.Insert(val._cart_pros.IndexOf(vmcartedit.pros), data);
                        val._cart_pros.Remove(vmcartedit.pros);
                    }
                }

                Navigation.RemovePage(this);

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