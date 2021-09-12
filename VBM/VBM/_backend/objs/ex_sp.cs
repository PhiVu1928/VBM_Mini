using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace vbm.objs
{
    public class extra_obj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public long id { get; set; }
        public string name_vn { get; set; }
        public string name_en { get; set; }
        public string alias { get; set; }
        public double price { get; set; }
        public List<long> mons { get; set; }
        int sl_;
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
    }

    public class spice_obj
    {
        public long ref_id { get; set; }
        public long id_default { get; set; }
        public string name_vn { get; set; }
        public string name_en { get; set; }
        public List<long> mons { get; set; }
        public List<spice_size_obj> lst_items { get; set; }

    }

    public class spice_size_obj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public long ref_id { get; set; }
        public long id { get; set; }
        public int size { get; set; }
        public string name_vn { get; set; }
        public string name_eng { get; set; }
        public string alias { get; set; }
        public double price { get; set; }
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
                OnPropertyChanged("Selected");
            }
        }
    }

    public class emenu_info_return
    {
        public List<extra_obj> extras { get; set; }
        public List<spice_obj> spices { get; set; }
    }

}
