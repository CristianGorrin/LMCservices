using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDGs
{
    public class RDGtblWorkerStatus
    {
        public List<InterfaceAdaptor.WorkerStatus> Get()
        {
            var list = new List<InterfaceAdaptor.WorkerStatus>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var tblWorkerStatus = dbContext.tblWorkerStatus;

                foreach (var item in tblWorkerStatus)
                {
                    var workerStatus = new InterfaceAdaptor.WorkerStatus()
                    {
                        StautsNo = item.statusNo,
                        Staus = item.status
                    };

                    list.Add(workerStatus);
                }
            }

            return list;
        }

        public InterfaceAdaptor.WorkerStatus Find(int stautsNumber)
        {
            var workerStatus = new InterfaceAdaptor.WorkerStatus();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var workerStatusFound = dbContext.tblWorkerStatus.SingleOrDefault(
                    x => x.statusNo == stautsNumber);

                workerStatus.Staus = workerStatusFound.status;
                workerStatus.StautsNo = workerStatusFound.statusNo;
            }

            return workerStatus;
        }

        public void Add(InterfaceAdaptor.WorkerStatus workerStatus)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var newWorkerStatus = new tblWorkerStatus()
                {
                    status = workerStatus.Staus
                };

                dbContext.tblWorkerStatus.InsertOnSubmit(newWorkerStatus);

                dbContext.SubmitChanges();
            }
        }

        public void Update(InterfaceAdaptor.WorkerStatus workerStatus)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var workerStatusUpdateing = dbContext.tblWorkerStatus.SingleOrDefault(
                    x => x.statusNo == workerStatus.StautsNo);

                workerStatusUpdateing.status = workerStatus.Staus;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var workerStatusDeleteing = dbContext.tblWorkerStatus.SingleOrDefault(
                    x => x.statusNo == id);

                var workerStatusDeletInfo = new StringBuilder();
                workerStatusDeletInfo.Append("[tblWorkerStatus] { ");
                workerStatusDeletInfo.Append("statusNo = " + workerStatusDeleteing.statusNo.ToString() + ", ");
                workerStatusDeletInfo.Append("status = " + workerStatusDeleteing.status + " }");

                var newDelete = new tblDeleteItem()
                {
                    deleteDate = DateTime.Now,
                    itemInfo = workerStatusDeletInfo.ToString(),
                    restored = false
                };

                dbContext.tblDeleteItems.InsertOnSubmit(newDelete);
                dbContext.tblWorkerStatus.DeleteOnSubmit(workerStatusDeleteing);

                dbContext.SubmitChanges();
            }
        }
    }
}
