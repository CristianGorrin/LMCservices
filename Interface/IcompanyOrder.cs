using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IcompanyOrder
    {
        Iworker CreateBy { get; set; }
        DateTime CreateDate { get; }
        IcompanyCustomer Customer { get; set; }
        DateTime DateSendBill { get; }
        int DaysToPaid { get; }
        string DescriptionTask { get; }
        DateTime HoutsUse { get; }
        int InvoiceNo { get; }
        bool Paid { get; }
        double PaidHour { get; }
        int PaidToAcc { get; }
        DateTime TaskDate { get; }
    }
}
