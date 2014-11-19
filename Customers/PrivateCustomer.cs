using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace Customers
{
    public class PrivateCustomer : Interface.IprivetCustomer
    {
        private bool active;
        private string altPhoneNo;
        private string email;
        private string homeAddress;
        private string name;
        private string phoneNo;
        private Interface.IpostNo postNo;
        private int privateCustomersNo;
        private string surname;

        public PrivateCustomer(int privateCustomersNo)
        {
            this.privateCustomersNo = privateCustomersNo;
        }

        public PrivateCustomer(PrivateCustomer obj)
        {
            this.active = obj.active;
            this.altPhoneNo = obj.altPhoneNo;
            this.email = obj.email;
            this.homeAddress = obj.homeAddress;
            this.name = obj.name;
            this.phoneNo = obj.phoneNo;
            this.postNo = obj.postNo;
            this.privateCustomersNo = obj.privateCustomersNo;
            this.surname = obj.surname;
        }

        public PrivateCustomer(bool active, string altPhoneNo, string email, string homeAddress, string name,
            string phoneNo, Interface.IpostNo postNo, int privateCustomersNo, string surname)
        {
            this.active = active;
            this.altPhoneNo = altPhoneNo;
            this.email = email;
            this.homeAddress = homeAddress;
            this.name = name;
            this.phoneNo = phoneNo;
            this.postNo = postNo;
            this.privateCustomersNo = privateCustomersNo;
            this.surname = surname;
        }

        public bool Active { get { return this.active; } set { this.active = value; } }
        public string AltPhoneNo { get { return this.altPhoneNo; } set { this.altPhoneNo = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
        public string HomeAddress { get { return this.homeAddress; } set { this.homeAddress = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string PhoneNo { get { return this.phoneNo; } set { this.phoneNo = value; } }
        public Interface.IpostNo PostNo { get { return this.postNo; } set { this.postNo = value; } }
        public int PrivateCustomersNo { get { return this.privateCustomersNo; } } // Is reed only use as index for DB
        public string Surname { get { return this.surname; } set { this.surname = value; } }
    }
}
