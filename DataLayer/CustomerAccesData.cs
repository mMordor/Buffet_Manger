using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CustomerAccesData
    {
        public ObservableCollection<Customer> customers { get; set; } = new ObservableCollection<Customer>();
        Accountung_DBEntities _db;



        public CustomerAccesData(Accountung_DBEntities db)
        {
            _db = db;
            GetCustomers();
        }

        void GetCustomers()
        {
            foreach (customers customer in _db.customers)
            {
                customers.Add(new Customer(customer.Id, customer.Name, customer.LastName, UInt64.Parse(customer.PhoneNumber), customer.Account));
            }
        }

        public void Updte(Customer np)
        {
            Customer temp = customers.First(p => p.Id == np.Id);
            customers[customers.IndexOf(temp)] = np;

            customers data = new customers()
            {
                Id = np.Id,
                Name = np.Name,
                LastName = np.LastName,
                PhoneNumber = np.PhoneNumber.ToString(),
                Account = np.Account,
            };
            var local = _db.Set<customers>().Local.FirstOrDefault(f => f.Id == np.Id);
            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }
            _db.Entry(data).State = EntityState.Modified;

        }

        public void Add(Customer p)
        {        
            customers.Add(p);

            customers data = new customers()
            {
                Id = p.Id,
                Name = p.Name,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber.ToString(),
                Account = p.Account,
            };
            _db.customers.Add(data);
        }

        public void Remove(int id)
        {
            Customer P = customers.First(p => p.Id == id);
            customers.Remove(P);

            customers data = _db.customers.First(p => p.Id == id);
            _db.customers.Remove(data);
        }

        public int NextID()
        {
            int nID = customers.Count > 0 ? customers.Max(c => c.Id) + 1 : 1;
            return nID;
        }

    }
}
