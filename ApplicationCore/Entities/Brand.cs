using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Brand : IAggregateRoot
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public ICollection<Product> Products { get; set; }

        public string PhotoPath { get; set; }
    }
}
