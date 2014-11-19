using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace Customers
{
    public class CompanyCustomer : Interface.IcompanyCustomer
    {
        private bool active;
        private string address;
        private string altPhoneNo;
        private int companyCustomersNo;
        private string contactPerson;
        private int cvrNo;
        private string email;
        private string name;
        private string phoneNo;
        private Interface.IpostNo postNo;

        public CompanyCustomer(int companyCustomersNo)
        {
            this.companyCustomersNo = companyCustomersNo;
        }

        public CompanyCustomer(CompanyCustomer obj)
        {
            this.active = obj.active;
            this.address = obj.address;
            this.altPhoneNo = obj.altPhoneNo;
            this.companyCustomersNo = obj.companyCustomersNo;
            this.contactPerson = obj.contactPerson;
            this.cvrNo = obj.cvrNo;
            this.email = obj.email;
            this.name = obj.name;
            this.phoneNo = obj.phoneNo;
            this.postNo = obj.postNo;
        }

        public CompanyCustomer(bool active, string address, string altPhoneNo, string contactPerson,
            int companyCustomersNo, int cvrNo, string email, string name, string phoneNo, Interface.IpostNo postNo)
        {
            this.active = active;
            this.address = address;
            this.altPhoneNo = altPhoneNo;
            this.contactPerson = contactPerson;
            this.companyCustomersNo = companyCustomersNo;
            this.cvrNo = cvrNo;
            this.email = email;
            this.name = name;
            this.phoneNo = phoneNo;
            this.postNo = postNo;
        }

        public string Address { get { return this.address; } set { this.address = value; } }
        public string AltPhoneNo { get { return this.altPhoneNo; } set { this.altPhoneNo = value; } }
        public bool Active { get { return this.active; } set { this.active = value; } }
        public int CompanyCustomersNo { get { return this.companyCustomersNo; } }// is reed only, use as index for DB
        public string ContactPerson { get { return this.contactPerson; } set { this.contactPerson = value; } }
        public int CvrNo { get { return this.cvrNo; } set { this.cvrNo = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string PhoneNo { get { return this.phoneNo; } set { this.phoneNo = value; } }
        public Interface.IpostNo PostNo { get { return this.postNo; } set { this.postNo = value; } }
    }
}
