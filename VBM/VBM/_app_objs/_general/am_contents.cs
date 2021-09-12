using System;
using System.Collections.Generic;
using VBM._pages._menu;
using System.Text;

namespace VBM._app_objs._general
{
    public class am_contents
    {
        public am_contents()
        {

        }
        public _pages._main.login_page Login_Page { get; set; }
        public _pages._main.start_page Start_Page { get; set; }
        public static menu_page menu_page { get; set; }
    }
}
