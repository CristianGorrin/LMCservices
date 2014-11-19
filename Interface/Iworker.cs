using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface Iworker
    {
        bool Active { get; }
        string Address { get; }
        string AltPhoneNo { get; }
        string Email { get; }
        string Name { get; }
        string PhoneNo { get; }
        IpostNo PostNo { get; }
        string Surname { get; }
        IworkerStatus WorkerStatus { get; set; }
        int WorkNo { get; }
    }
}
