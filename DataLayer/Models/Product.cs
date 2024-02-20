using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Product : Iproducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvalebleCount { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal income { get; set; }

        public Product() { }
        public Product(int  id,String name,int count,decimal buy,decimal sell) {
            Id = id;
            Name = name;
            AvalebleCount= count;
            BuyPrice= buy;
            SellPrice= sell;
            income=(sell*count)-(buy*count);
        }

    }
}
