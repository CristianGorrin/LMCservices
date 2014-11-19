using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace Company
{
    public class WorkerStatus : Interface.IworkerStatus
    {
        private string staus;
        private int stautsNo;

        public WorkerStatus(string staus, int stautsNo)
        {
            this.staus = staus;
            this.stautsNo = stautsNo;
        }

        public WorkerStatus(WorkerStatus obj)
        {
            this.staus = obj.staus;
            this.stautsNo = obj.stautsNo;
        }

        public WorkerStatus(int stautsNo)
        {
            this.stautsNo = stautsNo;
        }

        public string Staus { get { return this.staus; } set { this.staus = value; } }
        public int StautsNo { get { return this.stautsNo; } } // use for db so is reed only
    }
}
