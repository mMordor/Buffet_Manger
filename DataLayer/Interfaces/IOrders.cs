using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IOrders
    {
        int Id { get; set; }
        ObservableCollection<Product> ProductsList { get; set; }
        int CustomerId { get; set; }
        Decimal TotalPrice { get; set; }
    }
}
