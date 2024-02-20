using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Order : IOrders
    {
        public int Id { get; set; }
        public ObservableCollection<Product> ProductsList { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public decimal Balance { get; set; }

        public Order() { }
        public Order(int id ,ObservableCollection<Product> Pl,int cid) {
            Id = id;
            ProductsList = Pl;
            CustomerId = cid;
            foreach (var item in ProductsList)
            {
                TotalPrice += item.SellPrice;
            }
            Balance = TotalPrice;
        }
        public Order(int id, ObservableCollection<Product> Pl, int cid,decimal b)
        {
            Id = id;
            ProductsList = Pl;
            CustomerId = cid;
            Balance = b;
            foreach (var item in ProductsList)
            {
                TotalPrice += item.SellPrice;
            }
        }
    }
}
