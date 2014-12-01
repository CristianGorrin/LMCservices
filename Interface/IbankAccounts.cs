using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IbankAccounts
    {
        int Id { get; }
	    string Bank { get; }
	    string AccountName { get; }
	    int RegNo { get; }
	    string AccountNo { get; }
	    double Balance { get; }
    }
}
