using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IcompanyCustomer
    {
        string Address { get; }
        string AltPhoneNo { get; }
        bool Active { get; }
        int CompanyCustomersNo { get; }
        string ContactPerson { get; }
        int CvrNo { get; }
        string Email { get; }
        string Name { get; }
        string PhoneNo { get; }
        IpostNo PostNo { get; set; }
    }
}
