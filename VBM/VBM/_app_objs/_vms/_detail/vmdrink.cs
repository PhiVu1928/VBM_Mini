using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace VBM._app_objs._vms._detail
{
    public class vmdrink : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmdrink()
        {
            RenderDrink();
            RenderDrinkCombo();
            IsBusy = false;
        }

        void RenderDrink()
        {
            var mg = localdb._manager;
            var val = mg._varialbles;
            var drink = new ObservableRangeCollection<drinkEme>();
            foreach(var t1 in val._menus)
            {
                foreach(var t2 in t1.lst_sub_menu)
                {
                    foreach(var t3 in t2.lst_emes)
                    {
                        if (t1.id == 3)
                        {
                            drink.Add(new drinkEme(t3));
                        }
                    }
                }                
            }
            drinkEmes = drink;
        }

        void RenderDrinkCombo()
        {
            drinkcombos = new ObservableRangeCollection<drinkcombo>();
            for(int i = 0; i < 1; i++)
            {
                drinkcombos.Add(new drinkcombo(i));
            }
        }

        #region bien

        List<vbm.objs.drink_for_combo> lstdrink { get; set; } 
        ObservableRangeCollection<drinkcombo> drinkcombos_;
        ObservableRangeCollection<drinkEme> drinkEmes_;
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
        public ObservableRangeCollection<drinkcombo> drinkcombos
        {
            get
            {
                return drinkcombos_;
            }
            set
            {
                drinkcombos_ = value;
                OnPropertyChanged("drinkcombos");
            }
        }
        public ObservableRangeCollection<drinkEme> drinkEmes
        {
            get
            {
                return drinkEmes_;
            }
            set
            {
                drinkEmes_ = value;
                OnPropertyChanged("drinkEmes");
            }
        }
        #endregion
    }

    public class drinkcombo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public drinkcombo(int id)
        {
           if(id == 0)
            {
                name = "Classic";
                this.id = id;
                Selected = true;
            }
           if(id == 1)
            {
                name = "Healthy";
                this.id = id;
            }
        }
        public int id { get; set; }
        public string name { get; set; }
        Color TextColor_;
        Color BoxColor_;
        bool Selected_;
        public bool Selected
        {
            get
            {
                return Selected_;
            }
            set
            {
                Selected_ = value;
                if(value)
                {
                    TextColor = (Color)Application.Current.Resources["vbmgreen"];
                    BoxColor = (Color)Application.Current.Resources["vbmyellow"];
                }
                else
                {
                    TextColor = (Color)Application.Current.Resources["vbmlightgray"];
                    BoxColor = (Color)Application.Current.Resources["vbmpagebackground"];

                }
                OnPropertyChanged("Selected");
            }
        }
        public Color TextColor
        {
            get
            {
                return TextColor_;
            }
            set
            {
                TextColor_ = value;
                OnPropertyChanged("TextColor");
            }
        }
        public Color BoxColor
        {
            get
            {
                return BoxColor_;
            }
            set
            {
                BoxColor_ = value;
                OnPropertyChanged("BoxColor");
            }
        }
    }
    public class drinkEme : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnpropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public drinkEme(vbm.objs.e_menu_obj drk)
        {
            this.drk = drk;
            img = "http://manage.vuabanhmi.com/SpImg/" + drk.img;
            name = drk.name_vn;
            foreach(var items in drk.lst_size)
            {
                dongia = items.price.ToString("#,##0") + "đ";
                nguyengia = "9,000" + "đ";
            }
            thongtindinhduong = "Thông tin dinh dưỡng";
            chonTitle = "Chọn +";

        }

        public vbm.objs.e_menu_obj drk { get; set; }
        public string img { get; set; }
        public string name { get; set; }
        public string dongia { get; set; }
        public string nguyengia { get; set; }
        public string thongtindinhduong { get; set; }
        public string chonTitle { get; set; }

    }

}
