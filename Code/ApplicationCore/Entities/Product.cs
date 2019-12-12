using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Product : IAggregateRoot
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Sale { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }
        public int View { get; set; }
        public int Pay { get; set; }
        public int Hot { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<DetailOrder> DetailOrder { get; set; } //collection navigation property

    }
}
