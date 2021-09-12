using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBM._app_objs._general
{
    /// <summary>
    /// order_note: "regular": thuong, "upale", "cc", va ten cac loai khuyen mai code va qua code. Chu yeu se dung cai nay de group lai san pham
    /// order_type: 0: thuong, 55555: "upale", -1301: "cc", id cac loai khuyen mai va qua. Chu yeu se dung cai nay de group lai san pham
    /// order_name: thuong: de trong, upsale: San pham giam gia, cc: san pham lay sau, ten cac loai km va qua
    /// </summary>
    public class cart_pros 
    {
        public int slg { get; set; }
        public long id { get; set; }
        public long drinkid { get; set; }
        public int orderType { get; set; }
        public string name { get; set; }
        public List<cart_spice> spices { get; set; }
        public List<cart_ex> extras { get; set; }
        public double nguyengia { get; set; }
        public double dongia { get; set; }
        public double nuocdongia { get; set; }
        public bool isGetLater { get; set; }
        public bool isGetLaterOption { get; set; }
        public string orderCode { get; set; }
        public long groupID { get; set; }
        public string unique { get; set; }
        public cart_pros()
        {

        }
        public cart_pros clone()
        {
            var res = new cart_pros
            {
                nguyengia = nguyengia,
                dongia = dongia,
                groupID = groupID,
                id = id,
                drinkid = drinkid,
                name = name,
                isGetLater = isGetLater,
                isGetLaterOption = isGetLaterOption,
                extras = new List<cart_ex>(),
                spices = new List<cart_spice>(),
                orderCode = orderCode,
                orderType = orderType,
                slg = slg
            };
            extras.ForEach(p => res.extras.Add(p.Clone()));
            spices.ForEach(p => res.spices.Add(p.Clone()));
            return res;
        }

        public double renderprice()
        {
            double ng = (nguyengia * slg) + nuocdongia;
            extras.ForEach(p =>
            {
                ng += p.price * p.sl * slg;
            });
            return ng;
        }
        public static vbm.objs.e_menu_obj getDetailInfo(long id)
        {
            foreach (var t1 in localdb._manager._varialbles._menus)
            {
                foreach (var t2 in t1.lst_sub_menu)
                {
                    foreach (var t3 in t2.lst_emes)
                    {
                        foreach (var t4 in t3.lst_size)
                        {
                            if (t4.id == id)
                            {
                                return t3;
                            }
                        }
                    }
                }
            }
            return null;
        }
        public static vbm.objs.e_menu_obj getDrinkDetailInfo(long id)
        {
            foreach (var t1 in localdb._manager._varialbles._menus.Where(x => x.index == 3))
            {
                foreach (var t2 in t1.lst_sub_menu)
                {
                    foreach (var t3 in t2.lst_emes)
                    {
                        foreach(var t4 in t3.lst_size)
                        {
                            if(t4.id == id)
                            {
                                return t3;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static cart_pros createAddProd(long mainId, VBM._app_objs._vms._detail.DrinkSelected drinkSelected)
        {
            cart_pros data = null;
            var mg = localdb._manager;
            var val = mg._varialbles;
            foreach (var t1 in val._menus)
            {
                foreach (var t2 in t1.lst_sub_menu)
                {
                    foreach (var t3 in t2.lst_emes)
                    {
                        if (drinkSelected == null)
                        {
                            foreach (var t4 in t3.lst_size)
                            {
                                if (t4.id == mainId)
                                {
                                    data = new cart_pros
                                    {
                                        dongia = t4.price,
                                        groupID = t1.id,
                                        id = t4.id,
                                        name = t4.name_vn,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        extras = new List<cart_ex>(),
                                        spices = new List<cart_spice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                        slg = 1
                                    };
                                }
                            }
                        }
                        else if (drinkSelected.id > 0)
                        {
                            foreach (var t4 in t3.lst_size)
                            {
                                if (t4.id == mainId)
                                {
                                    data = new cart_pros
                                    {
                                        groupID = 0,
                                        dongia = t4.price,
                                        drinkid = drinkSelected.id,
                                        id = t4.id,
                                        nuocdongia = 9000,
                                        name = t4.name_vn,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        extras = new List<cart_ex>(),
                                        spices = new List<cart_spice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                        slg = 1
                                    };
                                    return data;
                                }
                            }
                        }
                        //case bat ky, drinkid <0
                        else
                        {
                            foreach (var t4 in t3.lst_size)
                            {
                                if (t4.id == mainId)
                                {
                                    data = new cart_pros
                                    {
                                        dongia = t4.price,
                                        groupID = t1.id,
                                        id = t4.id,
                                        drinkid = drinkSelected.id,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        extras = new List<cart_ex>(),
                                        spices = new List<cart_spice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                        slg = 1
                                    };
                                }
                            }
                            foreach (var t4 in t3.lst_combo)
                            {
                                if (t4.id == mainId)
                                {
                                    data = new cart_pros
                                    {
                                        groupID = 0,
                                        dongia = t4.price,
                                        id = t4.id,
                                        drinkid = drinkSelected.id,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        extras = new List<cart_ex>(),
                                        spices = new List<cart_spice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                        slg = 1
                                    };
                                    return data;
                                }
                            }
                        }
                    }
                }
            }
            return data;
        }


    }

    public class cart_ex 
    {
        public long id { get; set; }
        public double price { get; set; }
        public string name_vn { get; set; }
        public string name_en { get; set; }
        public int sl { get; set; }

        public cart_ex Clone()
        {
            return new cart_ex { name_en = name_en, name_vn = name_vn, id = id, price = price, sl = sl };
        }
    }

    public class cart_spice 
    {
        public long id { get; set; }
        public string name_vn { get; set; }
        public string name_en { get; set; }
        public cart_spice Clone()
        {
            return new cart_spice { id = id,name_vn = name_vn, name_en = name_en };
        }
    }

}
