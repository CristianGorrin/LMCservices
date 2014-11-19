using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IprivetCustomer
    {
        bool Active { get; }
        string AltPhoneNo { get; }
        string Email { get; }
        string HomeAddress { get; }
        string Name { get; }
        string PhoneNo { get; }
        IpostNo PostNo { get; set; }
        int PrivateCustomersNo { get; }
        string Surname { get; }
    }
}
