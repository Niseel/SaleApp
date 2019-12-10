using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Session
{
    public class CartSession
    {
        public int LoginID { get; set; }
        public string CartImage { get; set; }
        public int CartQty { get; set; }
        public int CartPrice { get; set; }

    }
}
