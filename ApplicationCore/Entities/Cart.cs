using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Cart
    {
        public int CartID { get; set; }
        public string CartName { get; set; }
        public string CartImage { get; set; }
        public int CartQty { get; set; }
        public decimal CartPrice { get; set; }
    }
}
