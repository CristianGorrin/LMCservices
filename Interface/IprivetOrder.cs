using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IprivetOrder
    {
        Iworker CreateBy { get; set; }
        DateTime CreateDate { get; }
        IprivetCustomer Customer { get; set; }
        DateTime DateSendBill { get; }
        int DaysToPaid { get; }
        string DescriptionTask { get; }
        DateTime HourUse { get; }
        int InvoiceNo { get; }
        bool Paid { get; }
        double PaidHour { get; }
        int PaidToAcc { get; }
        DateTime TaskDate { get; }
    }
}
