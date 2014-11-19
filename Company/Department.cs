using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace Company
{
    public class Department : Interface.Idepartment 
    {
        private bool active;
        private string address;
        private string altPhoneNo;
        private string companyName;
        private int cvrNo;
        private int department;
        private Worker departmentHead;
        private string email;
        private string phoneNo;
        private Interface.IpostNo postNo;

        public Department(bool active, string address, string altPhoneNo, string companyName, int cvrNo,
            int department, Worker departmentHead, string email, string phoneNo, Interface.IpostNo postNo)
        {
            this.active = active;
            this.address = address;
            this.altPhoneNo = altPhoneNo;
            this.companyName = companyName;
            this.cvrNo = cvrNo;
            this.department = department;
            this.departmentHead = departmentHead;
            this.email = email;
            this.phoneNo = phoneNo;
            this.postNo = postNo;
        }

        public Department(Department obj)
        {
            this.active = obj.active;
            this.address = obj.address;
            this.altPhoneNo = obj.altPhoneNo;
            this.companyName = obj.companyName;
            this.cvrNo = obj.cvrNo;
            this.department = obj.department;
            this.departmentHead = obj.departmentHead;
            this.email = obj.email;
            this.phoneNo = obj.phoneNo;
            this.postNo = obj.postNo;
        }

        public Department(int department)
        {
            this.department = department;
        }

        public bool Active { get { return this.active; } set { this.active = value; } }
        public string Address { get { return this.address; } set { this.address = value; } }
        public string AltPhoneNo { get { return this.altPhoneNo; } set { this.altPhoneNo = value; } }
        public string CompanyName { get { return this.companyName; } set { this.companyName = value; } }
        public int CvrNo { get { return this.cvrNo; } set { this.cvrNo = value; } }
        public int Deparment { get { return this.department; } } // use for db so is reed only
        public Interface.Iworker DeparmentHead { get { return this.departmentHead; } set { this.departmentHead = (Worker)value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
        public string PhoneNo { get { return this.phoneNo; } set { this.phoneNo = value; } }
        public Interface.IpostNo PostNo { get { return this.postNo; } set { this.postNo = value; } }
    }
}
