using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IinvoiceCompany
    {
        int Id { get; }
        bool Active { get; }
        Interface.IinvoiceCompany[] Order { get; }
    }
}
