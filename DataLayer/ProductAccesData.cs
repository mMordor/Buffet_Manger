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
    public class ProductAccesData
    {
        public ObservableCollection<Product> products { get; set; } = new ObservableCollection<Product>();
        Accountung_DBEntities _db ;

        public ProductAccesData(Accountung_DBEntities db)
        {
            _db = db;
            GetProducts();
        }
        void GetProducts()
        {
            foreach (products product in _db.products)
            {
                products.Add(new Product(product.Id, product.Name, product.AvalebleCount, product.BuyPrice, product.SellPrice));
            }
        }

        public void Updte(Product np)
        {          
            Product temp = products.First(p => p.Id == np.Id);
            products[products.IndexOf(temp)] = np;

            products data = new products()
            {
                Id = np.Id,
                Name = np.Name,
                AvalebleCount = np.AvalebleCount,
                BuyPrice = np.BuyPrice,
                SellPrice = np.SellPrice,
                income = np.income
            };
            var local = _db.Set<products>().Local.FirstOrDefault(f => f.Id == np.Id);
            if (local!= null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }
            _db.Entry(data).State = EntityState.Modified;
        }

        public void Add(Product p)
        {          
            products.Add(p);

            products data = new products()
            {
                Id=p.Id,
                Name=p.Name,
                AvalebleCount=p.AvalebleCount,
                BuyPrice=p.BuyPrice,
                SellPrice=p.SellPrice,
                income=p.income
            };
            _db.products.Add(data);
        }

        public void Remove(int id)
        {
            Product P = products.First(p => p.Id == id);
            products.Remove(P);

            products data = _db.products.First(p => p.Id == id);
            _db.products.Remove(data);
        }

        public int NextID()
        {
            int nID = products.Count > 0 ? products.Max(c => c.Id) + 1 : 1;
            return nID;
        }
    }
}
