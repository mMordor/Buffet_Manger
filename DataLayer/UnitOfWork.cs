using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork : IDisposable
    {
        Accountung_DBEntities _db = new Accountung_DBEntities();
        CustomerAccesData _cAccesData;
        OrderAccesData _oAccesData;
        ProductAccesData _pAccesData;

        public CustomerAccesData cAccesData
        {
            get
            {
                if (_cAccesData == null)
                {
                    _cAccesData = new CustomerAccesData(_db);
                }
                return _cAccesData;

            }
        }
        public OrderAccesData oAccesData
        {
            get
            {
                if (_oAccesData == null && _pAccesData != null)
                {
                    _oAccesData = new OrderAccesData(_db, _pAccesData);
                }
                if (_oAccesData == null && _pAccesData == null)
                {
                    _pAccesData = new ProductAccesData(_db);
                    _oAccesData = new OrderAccesData(_db, _pAccesData);
                }
                return _oAccesData;
            }
        }
        public ProductAccesData pAccesData
        {
            get
            {
                if (_pAccesData == null)
                {
                    _pAccesData = new ProductAccesData(_db);
                }
                return _pAccesData;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
