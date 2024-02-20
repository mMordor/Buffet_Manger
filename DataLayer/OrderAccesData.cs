using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public class OrderAccesData
    {
        public ObservableCollection<Order> Orders = new ObservableCollection<Order>();
        Accountung_DBEntities _db ;
        ProductAccesData existProducts;
        public OrderAccesData(Accountung_DBEntities db,ProductAccesData p)
        {
            existProducts = p;
            _db = db;
            GetOrders();
        }

        void GetOrders()
        {
            Orders.Clear();
            foreach (AccountingTable order in _db.AccountingTable)
            {
                ObservableCollection<Product> List = new ObservableCollection<Product>();
                String data = order.ProductsList;
                String[] pl = data.Split(';');
                foreach (String p in pl)
                {
                    if (p != "")
                    {
                        List.Add(existProducts.products.First(x => x.Id == int.Parse(p)));
                    }
                }
                Orders.Add(new Order(order.Id, List, order.CustomerId,order.Balance));
            }

        }

        public void Updte(Order np)
        {
            Order temp = Orders.First(p => p.Id == np.Id);
            Orders[Orders.IndexOf(temp)] = np;

            AccountingTable data = new AccountingTable()
            {
                Id = np.Id,
                CustomerId = np.CustomerId,
                TotalPrice = np.TotalPrice,
                Balance = np.Balance,
            };
            String List = String.Empty;
            foreach (Product p in np.ProductsList)
            {
                List += $"{p.Id}" + ";";
            }
            data.ProductsList = List;

            var local = _db.Set<AccountingTable>().Local.FirstOrDefault(f => f.Id == np.Id);
            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }
            _db.Entry(data).State = EntityState.Modified;

            //var nc = cdata.customers.First(x => x.Id == data.CustomerId);
            //nc.Account += temp.Balance - np.Balance;
            //cdata.Updte(nc);
        }

        public void Add(Order P)
        {
            Orders.Add(P);

            AccountingTable data = new AccountingTable();
            data.Id = P.Id;
            data.CustomerId = P.CustomerId;
            data.TotalPrice = P.TotalPrice;
            data.Balance = P.Balance;
            String List = String.Empty;
            foreach (Product p in P.ProductsList)
            {
                List += $"{p.Id}" + ";";
            }
            data.ProductsList = List;
            _db.AccountingTable.Add(data);

            //var nc = cdata.customers.First(x => x.Id == data.CustomerId);
            //nc.Account += data.Balance;
            //cdata.Updte(nc);

        }

        public void Remove(int id)
        {
            Order P = Orders.First(p => p.Id == id);
            Orders.Remove(P);

            AccountingTable data = _db.AccountingTable.First(p => p.Id == id);
            _db.AccountingTable.Remove(data);

            //var nc = cdata.customers.First(x => x.Id == data.CustomerId);
            //nc.Account -= data.Balance;
            //cdata.Updte(nc);

        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public int NextID()
        {
            int nID = Orders.Count > 0 ? Orders.Max(c => c.Id) + 1 : 1;
            return nID;
        }
    }
}
