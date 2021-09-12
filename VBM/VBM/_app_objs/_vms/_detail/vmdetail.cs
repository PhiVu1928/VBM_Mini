using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace VBM._app_objs._vms._detail
{
    public class vmdetail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmdetail(vbm.objs.e_menu_obj e_Menu_Obj)
        {
            SelectedItem = e_Menu_Obj;
            vsbdrinkrecom = true;
            vsbdrinkselected = false;
            RenderSize();
            RenderExtra();
            RenderSpice();
            sl = 1;
            foreach(var item in SelectedItem.lst_size)
            {
                if(item.size == 2 || item.size < 1)
                {
                    gia = item.price.ToString("#,##0") + "đ";
                }
            }
            IsBusy = false;
        }
        #region progress
        void RenderSize()
        {
            var Size_status_ = new ObservableRangeCollection<sizeStatus>();
            foreach(var item in SelectedItem.lst_size.OrderBy(x => x.price))
            {       
                if(item.size == 2 && item.size > 1)
                {
                    Size_status_.Add(new sizeStatus(item, true));
                    Selecteditemid = item.id;
                }
                else
                {
                    Size_status_.Add(new sizeStatus(item, false));
                }

            }    
            Size_status = Size_status_;
        }

        void RenderExtra()
        {
            var Extra_ = new ObservableRangeCollection<extraStatus>();
            var mg = localdb._manager;
            var val = mg._varialbles;
            foreach(var item in val._exts_spis.extras)
            {
                Extra_.Add(new extraStatus(item));
            }
            extras = Extra_;
        }
        void RenderSpice()
        {
            var _spices = new ObservableRangeCollection<spiceStatus>();
            var mg = localdb._manager;
            var val = mg._varialbles;
            try
            {
                foreach (var item in val._exts_spis.spices)
                {
                    var cond = false;
                    SelectedItem.lst_size.ForEach(p =>
                    {
                        if (item.mons.Contains(p.id))
                        {
                            cond = true;
                        }
                    });
                    if (cond)
                    {
                        _spices.Add(new spiceStatus(item));
                    }
                }
            }
            catch { }
            Spices = _spices;

        }
        public void RenderGia()
        {
            var Selecteditemprice = SelectedItem.lst_size.Where(x => x.id == Selecteditemid).FirstOrDefault();
            double cost = Selecteditemprice.price;
            if(DrinkSelected != null)
            {
                cost += DrinkSelected.dongia - 6000;
            }
            foreach(var items in extras)
            {
                cost += items.sl * items.extra.price;
            }
            cost *= sl;
            price = cost;
            gia = cost.ToString("#,##0")+"đ";
        }
        public void createSpiceSelects(spiceStatus spis)
        {
            try
            {
                Spice_Selects = new ObservableRangeCollection<spiceSelect>();
                foreach (var item in spis.spices.lst_items)
                {
                    Spice_Selects.Add(new spiceSelect(item, spis));
                }
            }
            catch { }
        }

        #endregion
        #region bien
        ObservableRangeCollection<sizeStatus> Size_status_;
        ObservableRangeCollection<spiceStatus> Spices_;
        ObservableRangeCollection<spiceSelect> Spices_select_;
        ObservableRangeCollection<extraStatus> extras_;
        DrinkSelected DrinkSelected_;
        bool vsbdrinkrecom_;
        bool vsbdrinkselected_;
        int sl_;
        string gia_;
        double price_;
        long Selecteditemid_;
        bool IsBusy_ = true;

        public bool IsBusy
        {
            get
            {
                return IsBusy_;
            }
            set
            {
                IsBusy_ = value;
                OnPropertyChanged("IsBusy");
            }
        }
        public double price
        {
            get
            {
                return price_;
            }
            set
            {
                price_ = value;
                OnPropertyChanged("price");
            }
        }
        public long Selecteditemid
        {
            get
            {
                return Selecteditemid_;
            }
            set
            {
                Selecteditemid_ = value;
                OnPropertyChanged("Selecteditemid");
            }
        }
        public int sl
        {
            get
            {
                return sl_;
            }
            set
            {
                sl_ = value;
                OnPropertyChanged("sl");
            }
        }
        public string gia
        {
            get
            {
                return gia_;
            }
            set
            {
                gia_ = value;
                OnPropertyChanged("gia");
            }
        }
        public DrinkSelected DrinkSelected
        {
            get
            {
                return DrinkSelected_;
            }
            set
            {
                DrinkSelected_ = value;
                OnPropertyChanged("DrinkSelected");
            }
        }
        public bool vsbdrinkrecom
        {
            get
            {
                return vsbdrinkrecom_;
            }
            set
            {
                vsbdrinkrecom_ = value;
                OnPropertyChanged("vsbdrinkrecom");
            }
        }
        public bool vsbdrinkselected
        {
            get
            {
                return vsbdrinkselected_;
            }
            set
            {
                vsbdrinkselected_ = value;
                OnPropertyChanged("vsbdrinkselected");
            }
        }
        public vbm.objs.e_menu_obj SelectedItem { get; set; }
        public ObservableRangeCollection<sizeStatus> Size_status
        {
            get
            {
                return Size_status_;
            }
            set
            {
                Size_status_ = value;
                OnPropertyChanged("Size_status");
            }
        }
        public ObservableRangeCollection<spiceStatus> Spices
        {
            get
            {
                return Spices_;
            }
            set
            {
                Spices_ = value;
                OnPropertyChanged("Spices");
            }
        }
        public ObservableRangeCollection<spiceSelect> Spice_Selects
        {
            get
            {
                return Spices_select_;
            }
            set
            {
                Spices_select_ = value;
                OnPropertyChanged("Spice_Selects");
            }
        }
        public ObservableRangeCollection<extraStatus> extras
        {
            get
            {
                return extras_;
            }
            set
            {
                extras_ = value;
                OnPropertyChanged("extras");
            }
        }

        #endregion
    }
    public class vmcartedit : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmcartedit(VBM._app_objs._general.cart_pros cart_Pros)
        {
            this.pros = cart_Pros;
            this.SelectedItem = VBM._app_objs._general.cart_pros.getDetailInfo(cart_Pros.id);
            Selecteditemid = pros.id;

            RenderSize();
            RenderExtra();
            RenderSpice();
            RenderDrink();
            sl = cart_Pros.slg;
            RenderGia();
            IsBusy = false;
        }
        #region progress
        void RenderSize()
        {
            var Size_status_ = new ObservableRangeCollection<sizeStatus>();
            int sizeSelected = SelectedItem.lst_size.Where(x => x.id == Selecteditemid).FirstOrDefault().size;
            foreach(var item in SelectedItem.lst_size.OrderBy(x => x.price))
            {       
                if(item.size == sizeSelected)
                {
                    Size_status_.Add(new sizeStatus(item, true));
                    Selecteditemid = item.id;
                }
                else
                {
                    Size_status_.Add(new sizeStatus(item, false));
                }

            }    
            Size_status = Size_status_;
        }

        void RenderExtra()
        {
            var Extra_ = new ObservableRangeCollection<extraStatus>();
            var mg = localdb._manager;
            var val = mg._varialbles;
            if(SelectedItem.class_id == 1)
            {
                foreach (var item in val._exts_spis.extras)
                {
                    Extra_.Add(new extraStatus(item,pros));
                }
            }            
            extras = Extra_;
        }
        void RenderSpice()
        {
            var _spices = new ObservableRangeCollection<spiceStatus>();
            var mg = localdb._manager;
            var val = mg._varialbles;
            try
            {
                foreach (var item in val._exts_spis.spices)
                {
                    var cond = false;
                    foreach (var t in SelectedItem.lst_size)
                    {
                        if (item.mons.Contains(t.id))
                        {
                            cond = true;
                            break;
                        }
                    }

                    if (cond)
                    {
                        _spices.Add(new spiceStatus(item, pros.spices));
                    }
                }
            }
            catch { }
            Spices = _spices;

        }
        void RenderDrink()
        {
            vsbdrinkrecom = pros.orderType >= 0 ? pros.groupID != 0 ? SelectedItem.class_id == 1 ? true : false : false : false;

            vsbdrinkselected = pros.groupID == 0 ? true : false;
            if (vsbdrinkselected)
            {
                DrinkSelected = new DrinkSelected();
                DrinkSelected.DrinkCartSelectedChange(pros);
            }
        }
        public void RenderGia()
        {
            var Selecteditemprice = SelectedItem.lst_size.Where(x => x.id == Selecteditemid).FirstOrDefault();
            double cost = Selecteditemprice.price;
            if(DrinkSelected != null)
            {
                cost += DrinkSelected.dongia - 6000;
            }
            foreach(var items in extras)
            {
                cost += items.sl * items.extra.price;
            }
            cost *= sl;
            price = cost;
            gia = cost.ToString("#,##0")+"đ";
        }
        public void createSpiceSelects(spiceStatus spis)
        {
            try
            {
                Spice_Selects = new ObservableRangeCollection<spiceSelect>();
                foreach (var item in spis.spices.lst_items)
                {
                    Spice_Selects.Add(new spiceSelect(item, spis));
                }
            }
            catch { }
        }

        #endregion
        #region bien
        ObservableRangeCollection<sizeStatus> Size_status_;
        ObservableRangeCollection<spiceStatus> Spices_;
        ObservableRangeCollection<spiceSelect> Spices_select_;
        ObservableRangeCollection<extraStatus> extras_;
        DrinkSelected DrinkSelected_;
        bool vsbdrinkrecom_;
        bool vsbdrinkselected_;
        int sl_;
        string gia_;
        double price_;
        long Selecteditemid_;
        bool IsBusy_ = true;

        public bool IsBusy
        {
            get
            {
                return IsBusy_;
            }
            set
            {
                IsBusy_ = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public double price
        {
            get
            {
                return price_;
            }
            set
            {
                price_ = value;
                OnPropertyChanged("price");
            }
        }
        public long Selecteditemid
        {
            get
            {
                return Selecteditemid_;
            }
            set
            {
                Selecteditemid_ = value;
                OnPropertyChanged("Selecteditemid");
            }
        }
        public int sl
        {
            get
            {
                return sl_;
            }
            set
            {
                sl_ = value;
                OnPropertyChanged("sl");
            }
        }
        public string gia
        {
            get
            {
                return gia_;
            }
            set
            {
                gia_ = value;
                OnPropertyChanged("gia");
            }
        }
        public DrinkSelected DrinkSelected
        {
            get
            {
                return DrinkSelected_;
            }
            set
            {
                DrinkSelected_ = value;
                OnPropertyChanged("DrinkSelected");
            }
        }
        public bool vsbdrinkrecom
        {
            get
            {
                return vsbdrinkrecom_;
            }
            set
            {
                vsbdrinkrecom_ = value;
                OnPropertyChanged("vsbdrinkrecom");
            }
        }
        public bool vsbdrinkselected
        {
            get
            {
                return vsbdrinkselected_;
            }
            set
            {
                vsbdrinkselected_ = value;
                OnPropertyChanged("vsbdrinkselected");
            }
        }
        public VBM._app_objs._general.cart_pros pros { get; set; }
        public vbm.objs.e_menu_obj SelectedItem { get; set; }
        public ObservableRangeCollection<sizeStatus> Size_status
        {
            get
            {
                return Size_status_;
            }
            set
            {
                Size_status_ = value;
                OnPropertyChanged("Size_status");
            }
        }
        public ObservableRangeCollection<spiceStatus> Spices
        {
            get
            {
                return Spices_;
            }
            set
            {
                Spices_ = value;
                OnPropertyChanged("Spices");
            }
        }
        public ObservableRangeCollection<spiceSelect> Spice_Selects
        {
            get
            {
                return Spices_select_;
            }
            set
            {
                Spices_select_ = value;
                OnPropertyChanged("Spice_Selects");
            }
        }
        public ObservableRangeCollection<extraStatus> extras
        {
            get
            {
                return extras_;
            }
            set
            {
                extras_ = value;
                OnPropertyChanged("extras");
            }
        }

        #endregion
    }
    public class spiceStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public spiceStatus(vbm.objs.spice_obj spices, List<VBM._app_objs._general.cart_spice> cartProdSpices = null)
        {
            this.spices = spices;
            vbm.objs.spice_size_obj ch = null;
            if (cartProdSpices == null)
            {
                ch = spices.lst_items.Where(p => p.size == 2).FirstOrDefault();
                if (ch == null)
                {
                    ch = spices.lst_items.OrderBy(p => p.size).ToList()[0];
                }
            }
            else
            {
                foreach (var item in spices.lst_items)
                {
                    var has = cartProdSpices.Where(p => p.id == item.id).FirstOrDefault();
                    if (has != null)
                    {
                        ch = item;
                        break;
                    }
                }
                if (ch == null)
                {
                    ch = spices.lst_items.Where(p => p.size == 2).FirstOrDefault();
                    if (ch == null)
                    {
                        ch = spices.lst_items.OrderBy(p => p.size).ToList()[0];
                    }
                }
            }
            name = ch.name_vn;
        }
        

        string name_;
        public string name
        {
            get
            {
                return name_;
            }
            set
            {
                name_ = value;
                OnPropertyChanged("name");
            }
        }
        public vbm.objs.spice_obj spices { get; set; }
        public long idSelected { get; set; }

    }

    public class spiceSelect
    {
        public spiceSelect(vbm.objs.spice_size_obj item, spiceStatus spiStatus)
        {
            this.spiStatus = spiStatus;
            this.item = item;
            this.name = item.name_vn;
        }

        public vbm.objs.spice_size_obj item { get; set; }
        public spiceStatus spiStatus { get; set; }
        public string name { get; set; }

    }
    public class sizeStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public sizeStatus(vbm.objs.e_menu_size_obj eme, bool selected)
        {
            this.eme = eme;
            this.Selected = selected;
            string n = "";
            if (eme.size == 1)
            {
                n += "S - ";
            }
            else if (eme.size == 2)
            {
                n += "M - ";
            }
            else if (eme.size == 3)
            {
                n += "L - ";
            }
            n += eme.price.ToString("#,##0") + " đ";
            name = n;
        }
        public string name { get; set; }

        Color _Textcolor;
        Color _BorderBackgroundcolor;
        Color _Bordercolor;
        bool _Selected;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                if (value)
                {
                    TextColor = (Color)Application.Current.Resources["vbmpinttext"];
                    BorderColor = (Color)Application.Current.Resources["vbmdeeplightgray"];
                    BorderBackgroundColor = (Color)Application.Current.Resources["vbmpinttext"];
                }
                else
                {
                    TextColor = (Color)Application.Current.Resources["vbmdeepgray"];
                    BorderColor = (Color)Application.Current.Resources["vbmpagebackground"];
                    BorderBackgroundColor = (Color)Application.Current.Resources["vbmpagebackground"];
                }
            }
        }
        public Color BorderBackgroundColor
        {
            get
            {
                return _BorderBackgroundcolor;
            }
            set
            {
                _BorderBackgroundcolor = value;
                OnPropertyChanged("BorderBackgroundColor");
            }
        }
        public Color BorderColor
        {
            get
            {
                return _Bordercolor;
            }
            set
            {
                _Bordercolor = value;
                OnPropertyChanged("BorderColor");
            }
        }
        public Color TextColor
        {
            get
            {
                return _Textcolor;
            }
            set
            {
                _Textcolor = value;
                OnPropertyChanged("TextColor");
            }
        }
        public vbm.objs.e_menu_size_obj eme { get; set; }

    }

    public class extraStatus : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public extraStatus(vbm.objs.extra_obj extra, VBM._app_objs._general.cart_pros cartProd = null)
        {
            this.extra = extra;
            name = extra.name_vn;
            price = extra.price.ToString("#,##0") + " đ";
            if (cartProd != null)
            {
                var curE = cartProd.extras.Where(p => p.id == extra.id).FirstOrDefault();
                if (curE != null)
                {
                    sl = curE.sl;
                }
            }
        }

        int sl_;

        public string name { get; set; }
        public int sl
        {
            get
            {
                return sl_;
            }
            set
            {
                sl_ = value;
                OnPropertyChanged("sl");
            }
        }
        public string price { get; set; }
        public vbm.objs.extra_obj extra { get; set; }

    }

    public class DrinkSelected : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public DrinkSelected()
        {
            changeTitle = "Thay đổi";
            huyTitle = "Hủy";
        }

        public void DrinkSelectedChange(vbm.objs.e_menu_obj drinkselected)
        {
            this.drinkseleceted = drinkseleceted;
            img = "http://manage.vuabanhmi.com/SpImg/" + drinkselected.img;
            name = drinkselected.name_vn;
            foreach(var t1 in drinkselected.lst_size)
            {
                dongia = t1.price;
                moreCost = "+ 9,000 đ"; 
                id = t1.id;

            }
        }
        public void DrinkCartSelectedChange(VBM._app_objs._general.cart_pros cartdrinkselected)
        {
            this.drinkseleceted = VBM._app_objs._general.cart_pros.getDrinkDetailInfo(cartdrinkselected.drinkid);
            if(drinkseleceted != null)
            {
                id = drinkseleceted.index;
                img = "http://manage.vuabanhmi.com/SpImg/" + drinkseleceted.img;
                name = drinkseleceted.name_vn;
                foreach (var t1 in drinkseleceted.lst_size)
                {
                    dongia = t1.price;
                    moreCost = "+ 9,000 đ";

                }
            }            
        }


        string img_;
        string name_;
        string moreCost_;

        public vbm.objs.e_menu_obj drinkseleceted { get; set; }
        public string img
        {
            get
            {
                return img_;
            }
            set
            {
                img_ = value;
                OnPropertyChanged("img");
            }
        }
        public string name
        {
            get
            {
                return name_;
            }
            set
            {
                name_ = value;
                OnPropertyChanged("name");
            }
        }
        public string moreCost
        {
            get
            {
                return moreCost_;
            }
            set
            {
                moreCost_ = value;
                OnPropertyChanged("moreCost");
            }
        }
        public double dongia { get; set; }
        public long id { get; set; }
        public string changeTitle { get; set; }
        public string huyTitle { get; set; }

    }


}
