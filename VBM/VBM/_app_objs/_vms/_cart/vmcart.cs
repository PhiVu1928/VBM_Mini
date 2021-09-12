using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace VBM._app_objs._vms._cart
{
    public class vmcart : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmcart()
        {
            RenderCart();
            Rendertongtien();
            IsBusy = false;
        }

        public void RenderCart()
        {
            Cartitems = new ObservableRangeCollection<cartitem>();
            foreach(var item in localdb._manager._varialbles._cart_pros)
            {
                Cartitems.Add(new cartitem(item));
            }
        }

        public (double,double) Rendertongtien()
        {
            double tgt = 0;
            double tht = 0;
            foreach (var t1 in localdb._manager._varialbles._cart_pros)
            {
                var cal = t1.renderprice();
                tgt += cal;
            }
            tongtien = tgt.ToString("#,##0") + " đ";
            thanhtien = tgt.ToString("#,##0") + " đ";
            giamgia = (tgt - tht).ToString("#,##0") + " đ";
            return (tgt, tht);
        }

        string tongtien_;
        string thanhtien_;
        string giamgia_;
        ObservableRangeCollection<cartitem> cartitems_;
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
        public string tongtien
        {
            get
            {
                return tongtien_;
            }
            set
            {
                tongtien_ = value;
                OnPropertyChanged("tongtien");
            }
        }
        public string thanhtien
        {
            get
            {
                return thanhtien_;
            }
            set
            {
                thanhtien_ = value;
                OnPropertyChanged("thanhtien");
            }
        }
        public string giamgia
        {
            get
            {
                return giamgia_;
            }
            set
            {
                giamgia_ = value;
                OnPropertyChanged("giamgia");
            }
        }
        public ObservableRangeCollection<cartitem> Cartitems
        {
            get
            {
                return cartitems_;
            }
            set
            {
                cartitems_ = value;
                OnPropertyChanged("Cartitems");
            }
        }
    }



    public class cartitem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public cartitem(VBM._app_objs._general.cart_pros pros)
        {
            this.pros = pros;
            sl = pros.slg;
            monchinh_extrasRender = new List<cartExtraVM>();
            monchinh_giavi = "";
            drink_giavi = "";
            vbm.objs.e_menu_obj emenu = VBM._app_objs._general.cart_pros.getDetailInfo(pros.id);
            if(emenu != null)
            {
                if(pros.groupID == 0)
                {
                    //mon chinh
                    monchinh_img = "http://manage.vuabanhmi.com/SpImg/" + emenu.img;
                    monchinh_name = pros.name;
                    monchinh_visnguyengia = false;
                    monchinh_dongia = pros.dongia.ToString("#,##0") + "đ";
                    foreach (var item in pros.extras)
                    {
                        monchinh_extrasRender.Add(new cartExtraVM(item));
                    }
                    var spices = localdb._manager._varialbles._exts_spis.spices.Where(x => x.mons.Contains(emenu.index)).ToList();
                    foreach (var item in pros.spices)
                    {
                        foreach (var t2 in spices)
                        {
                            var spiSize = t2.lst_items.Where(x => x.id == item.id).FirstOrDefault();
                            if (spiSize != null)
                            {
                                monchinh_giavi += item.name_vn + ", ";
                                break;
                            }
                        }
                    }

                    //nuoc
                    visDrinkCb = true;
                    var nuocfull = VBM._app_objs._general.cart_pros.getDrinkDetailInfo(pros.drinkid);
                    vbm.objs.e_menu_size_obj nuocdetail = nuocfull != null ? nuocfull.lst_size.Where(x => x.size == 0).FirstOrDefault() : null;
                    if (nuocfull != null && nuocdetail != null)
                    {
                        drinkImg = "http://manage.vuabanhmi.com/SpImg/" + nuocfull.img;
                        drinkname = nuocfull.name_vn;
                        drink_nguyengia = nuocdetail.price.ToString("#,##0") + "đ";
                        drink_dongia = "9,000đ";
                    }
                }
                else
                {
                    //mon chinh
                    monchinh_img = "http://manage.vuabanhmi.com/SpImg/" + emenu.img;
                    monchinh_name = pros.name;
                    monchinh_visnguyengia = false;
                    monchinh_dongia = pros.dongia.ToString("#,##0") + "đ";
                    foreach (var item in pros.extras)
                    {
                        monchinh_extrasRender.Add(new cartExtraVM(item));
                    }
                    var spices = localdb._manager._varialbles._exts_spis.spices.Where(x => x.mons.Contains(emenu.index)).ToList();
                    foreach (var item in pros.spices)
                    {
                        foreach (var t2 in spices)
                        {
                            var spiSize = t2.lst_items.Where(x => x.id == item.id).FirstOrDefault();
                            if (spiSize != null)
                            {
                                monchinh_giavi += item.name_vn + ", ";
                                break;
                            }
                        }
                    }
                    visRecommendCb = true;
                }               
            }
        }
        public VBM._app_objs._general.cart_pros pros { get; set; }
        public vbm.objs.e_menu_obj prosinfo { get; set; }
        public int sl_;
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

        public string monchinh_img { get; set; }
        public string monchinh_name { get; set; }
        public string monchinh_nguyengia { get; set; }
        public bool monchinh_visnguyengia { get; set; }
        public string monchinh_dongia { get; set; }
        public List<cartExtraVM> monchinh_extrasRender { get; set; }
        public string monchinh_giavi { get; set; }

        public bool visDrinkCb { get; set; }
        public string drinkImg { get; set; }
        public string drinkname { get; set; }
        public string drink_giavi { get; set; }
        public string drink_nguyengia { get; set; }
        public string drink_dongia { get; set; }

        public bool visRecommendCb { get; set; }
    }
    public class cartExtraVM
    {
        public cartExtraVM(VBM._app_objs._general.cart_ex extra)
        {
            solg = extra.sl;
            name = extra.name_vn + "x "+ solg ;  
        }

        public int solg { get; set; }
        public string name { get; set; }

    }
}
