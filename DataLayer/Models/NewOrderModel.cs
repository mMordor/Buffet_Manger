using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class NewOrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public decimal Balance { get; set; }

        public NewOrderModel() { }
    }
}
