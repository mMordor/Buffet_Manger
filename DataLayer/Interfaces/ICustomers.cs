using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ICustomers
    {
        int Id { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        UInt64  PhoneNumber { get; set; }
        Decimal Account { get; set; }
    }
}
