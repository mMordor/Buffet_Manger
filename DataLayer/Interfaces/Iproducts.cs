using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface Iproducts
    {
        int Id { get; set; }
        String Name { get; set; }
        int AvalebleCount   { get; set; }
        Decimal BuyPrice { get; set; }
        Decimal SellPrice { get; set; }
        Decimal income { get; set; }
    }
}
