using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace VBM._app_objs._vms._home
{
    public class vmmenu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmmenu()
        {
            LoadMoreItemsCommand = new Command(LoadMoreItems);
            createtabitem();
            IsBusy = false;
        }

        #region Progess


        void createtabitem()
        {
            var mg = localdb._manager;
            var val = mg._varialbles;
            sfTabItems = new TabItemCollection();
            foreach(var item in val._menus)
            {
                foreach(var sub in item.lst_sub_menu)
                {
                    if (item.lst_sub_menu.Count == 1)
                    {
                        SfTabItem tabItem = new SfTabItem();
                        tabItem.Title = item.name_vn;
                        sfTabItems.Add(tabItem);
                    }
                    else
                    {
                        SfTabItem tabItem = new SfTabItem();
                        tabItem.Title = sub.name_vn;
                        sfTabItems.Add(tabItem);
                    }
                }    
            }
        }

        public async void createitem(List<vbm.objs.e_menu_obj> e_Menu_Obj)
        {
            menu_temps = new ObservableRangeCollection<vbm.objs.e_menu_obj>();
            foreach(var item in e_Menu_Obj.Where(x => x.img != ""))
            {
                menu_temps.Add(item);
            }
            await Task.Run(() => { RenderMenu(0, 9); });
        }
        public async void RenderMenu(int page,int pagesize)
        {
            main_menus = new ObservableRangeCollection<vbm.objs.e_menu_obj>();
            foreach(var items in menu_temps.Skip(page).Take(pagesize))
            {
                main_menus.Add(items);
            }
        }

        public async void LoadMoreItems()
        {
            IsBusy = true;
            await Task.Delay(500);
            var index = main_menus.Count();
            if(index < menu_temps.Count())
            {
                foreach(var items in menu_temps.Skip(index).Take(9))
                {
                    main_menus.Add(items);
                }
            }
            else { }
            IsBusy = false;
        }
        #endregion

        #region Bien



        int slcart_;
        public int slcart
        {
            get
            {
                return slcart_;
            }
            set
            {
                slcart_ = value;
                OnPropertyChanged("slcart");
            }
        }
        public Command LoadMoreItemsCommand { get; set; }
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

        public ObservableRangeCollection<vbm.objs.e_menu_obj> menu_temps { get; set; }

        ObservableRangeCollection<vbm.objs.e_menu_obj> main_menus_;
        public ObservableRangeCollection<vbm.objs.e_menu_obj> main_menus
        {
            get
            {
                return main_menus_;
            }
            set
            {
                main_menus_ = value;
                OnPropertyChanged("menus");
            }
        }

        TabItemCollection sfTabItems_;

        public TabItemCollection sfTabItems
        {
            get
            {
                return sfTabItems_;
            }
            set
            {
                sfTabItems_ = value;
                OnPropertyChanged("sfTabItems");
            }
        }
        #endregion
    }
}
