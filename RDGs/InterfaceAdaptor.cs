using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace RDGs
{
    public class InterfaceAdaptor
    {
        public class PostNo : Interface.IpostNo
        {
            public string City { get; set; }
            public int Id { get; set; }
            public int PostNumber { get; set; }
        }

        public class Department : Interface.Idepartment
        {
            public bool Active { get; set; }
            public string Address { get; set; }
            public string AltPhoneNo { get; set; }
            public string CompanyName { get; set; }
            public int CvrNo { get; set; }
            public int Deparment { get; set; }
            public Iworker DeparmentHead { get; set; }
            public string Email { get; set; }
            public string PhoneNo { get; set; }
            public IpostNo PostNo { get; set; }
        }

        public class CompanyCustomer : Interface.IcompanyCustomer
        {
            public string Address { get; set; }
            public string AltPhoneNo { get; set; }
            public bool Active { get; set; }
            public int CompanyCustomersNo { get; set; }
            public string ContactPerson { get; set; }
            public int CvrNo { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string PhoneNo { get; set; }
            public IpostNo PostNo { get; set; }
        }

        public class CompanyOrder : Interface.IcompanyOrder
        {
            public Iworker CreateBy { get; set; }
            public DateTime CreateDate { get; set; }
            public IcompanyCustomer Customer { get; set; }
            public DateTime DateSendBill { get; set; }
            public int DaysToPaid { get; set; }
            public string DescriptionTask { get; set; }
            public double HoutsUse { get; set; }
            public int InvoiceNo { get; set; }
            public bool Paid { get; set; }
            public double PaidHour { get; set; }
            public int PaidToAcc { get; set; }
            public DateTime TaskDate { get; set; }
        }

        public class PrivetCustomer : Interface.IprivetCustomer
        {
            public bool Active { get; set; }
            public string AltPhoneNo { get; set; }
            public string Email { get; set; }
            public string HomeAddress { get; set; }
            public string Name { get; set; }
            public string PhoneNo { get; set; }
            public IpostNo PostNo { get; set; }
            public int PrivateCustomersNo { get; set; }
            public string Surname { get; set; }
        }

        public class PrivetOrder : Interface.IprivetOrder
        {
            public Iworker CreateBy { get; set; }
            public DateTime CreateDate { get; set; }
            public IprivetCustomer Customer { get; set; }
            public DateTime? DateSendBill { get; set; }
            public int DaysToPaid { get; set; }
            public string DescriptionTask { get; set; }
            public double HourUse { get; set; }
            public int InvoiceNo { get; set; }
            public bool Paid { get; set; }
            public double PaidHour { get; set; }
            public int PaidToAcc { get; set; }
            public DateTime TaskDate { get; set; }
        }

        public class Worker : Interface.Iworker
        {
            public bool Active { get; set; }
            public string Address { get; set; }
            public string AltPhoneNo { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string PhoneNo { get; set; }
            public IpostNo PostNo { get; set; }
            public string Surname { get; set; }
            public IworkerStatus WorkerStatus { get; set; }
            public int WorkNo { get; set; }
        }

        public class WorkerStatus : Interface.IworkerStatus
        {
            public string Staus { get; set; }
            public int StautsNo { get; set; }
        }

        public class BankAccounts : Interface.IbankAccounts
        {
            public int Id { get; set; }
            public string Bank { get; set; }
            public string AccountName { get; set; }
            public int RegNo { get; set; }
            public string AccountNo { get; set; }
            public double Balance { get; set; }
        }

        public class InvoiceCompany : Interface.IinvoiceCompany
        {
            public int Id { get; set; }
            public bool Active { get; set; }
            public int?[] Order { get; set; }
        }

        public class InvoicePrivet : Interface.IinvoicePrivet
        {
            public int Id { get; set; }
            public bool Active { get; set; }
            public int?[] Order { get; set; }
        }
    }
}
