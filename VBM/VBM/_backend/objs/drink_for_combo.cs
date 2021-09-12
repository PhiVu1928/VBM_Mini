using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using vbm;
using vbm.objs;

namespace vbm.objs
{
    class drink_for_combo
    {   

        public class drink_cb
        {
            public long spId { get; set; }
            public Dictionary<string, string> name { get; set; }
            public long classID { get; set; }
            public Dictionary<string, string> className { get; set; }
            public string img { get; set; }
            public double nguyengia { get; set; }
            public double dongia { get; set; }

            public drink_cb clone()
            {
                return new drink_cb { classID = classID, className = className, dongia = dongia, nguyengia = nguyengia, img = img, name = new Dictionary<string, string>(name), spId = spId };
            }

            public static async Task<List<drink_cb>> getdrinkcombo(long id)
            {
                string url = input_data.drink_for_combo + $"?channel=1&mainID={id}";
                using (var cl = new HttpClient())
                {
                    var res1 = await cl.GetAsync(url);
                    var res2 = await res1.Content.ReadAsStringAsync();
                    var job = JObject.Parse(res2);
                    var success = bool.Parse(tools.GetJArrayValue(job, "Success"));
                    if (success)
                    {
                        var datas = tools.GetJArrayValue(job, "Datas");
                        return JsonConvert.DeserializeObject<List<drink_cb>>(datas);
                    }
                }
                return null;

            }
        }
    }
}
