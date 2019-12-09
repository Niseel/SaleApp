using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class User : IAggregateRoot
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public DateTime Birth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AvtPath { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int Level { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
