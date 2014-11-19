using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace Company
{
    public class Worker : Interface.Iworker
    {
        private bool active;
        private string address;
        private string altPhoneNo;
        private string email;
        private string name;
        private string phoneNo;
        private Interface.IpostNo postNo;
        private string surname;
        private WorkerStatus workerStatus;
        private int workNo;

        public Worker(bool active, string address, string altPhoneNo, string email, string name, string phoneNo,
            Interface.IpostNo postNo, string surname, WorkerStatus workerStatus, int workNo)
        {
            this.active = active;
            this.address = address;
            this.altPhoneNo = altPhoneNo;
            this.email = email;
            this.name = name;
            this.phoneNo = phoneNo;
            this.postNo = postNo;
            this.surname = surname;
            this.workerStatus = workerStatus;
            this.workNo = workNo;
        }

        public Worker(Worker obj)
        {
            this.active = obj.active;
            this.address = obj.address;
            this.altPhoneNo = obj.altPhoneNo;
            this.email = obj.email;
            this.name = obj.name;
            this.phoneNo = obj.phoneNo;
            this.postNo = obj.postNo;
            this.surname = obj.surname;
            this.workerStatus = obj.workerStatus;
            this.workNo = obj.workNo;
        }

        public Worker(int workerNo)
        {
            this.workNo = workerNo;
        }

        public bool Active { get { return this.active; } set { this.active = value; } }
        public string Address { get { return this.address; } set { this.address = value; } }
        public string AltPhoneNo { get { return this.altPhoneNo; } set { this.altPhoneNo = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string PhoneNo { get { return this.phoneNo; } set { this.phoneNo = value; } }
        public Interface.IpostNo PostNo { get { return this.postNo; } set { this.postNo = value; } }
        public string Surname { get { return this.surname; } set { this.surname = value; } }
        public Interface.IworkerStatus WorkerStatus { get { return this.workerStatus; } set { this.workerStatus = (WorkerStatus)value; } }
        public int WorkNo { get { return this.workNo; } } // use for db so is reed only
    }
}
