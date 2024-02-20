using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Customer : ICustomers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public UInt64 PhoneNumber { get; set; }
        public decimal Account { get; set; }

        public Customer() { }
        public Customer(int id,String name,String lname,UInt64 p,decimal account)
        {
            Id = id;
            Name = name;
            LastName = lname;
            PhoneNumber = p;
            Account = account;
        }
    }
}
