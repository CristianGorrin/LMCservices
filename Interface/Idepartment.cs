using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface Idepartment
    {
        bool Active { get; }
        string Address { get; }
        string AltPhoneNo { get; }
        string CompanyName { get; }
        int CvrNo { get; }
        int Deparment { get; }
        Iworker DeparmentHead { get; }
        string Email { get; }
        string PhoneNo { get; }
        IpostNo PostNo { get; }
    }
}
