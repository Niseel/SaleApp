using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Order : IAggregateRoot
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int MethodPaying { get; set; }
        public string BankBrand { get; set; }
        public string CardNumber { get; set; }
        public string Note { get; set; }

        public int Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public int UserID { get; set; }
        public int AdminID { get; set; }

        public ICollection<DetailOrder> DetailOrder { get; set; } //collection navigation property
    }
}
