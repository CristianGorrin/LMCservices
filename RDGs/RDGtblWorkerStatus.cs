using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

namespace RDGs
{
    public class RDGtblWorkerStatus
    {
        private string connectionString;

        public RDGtblWorkerStatus(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Interface.IworkerStatus> Get()
        {
            var list = new List<Interface.IworkerStatus>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
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

        public Interface.IworkerStatus Find(int stautsNumber)
        {
            var workerStatus = new InterfaceAdaptor.WorkerStatus();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var workerStatusFound = dbContext.tblWorkerStatus.SingleOrDefault(
                    x => x.statusNo == stautsNumber);

                workerStatus.Staus = workerStatusFound.status;
                workerStatus.StautsNo = workerStatusFound.statusNo;
            }

            return workerStatus;
        }

        public void Add(Interface.IworkerStatus workerStatus)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var newWorkerStatus = new tblWorkerStatus()
                {
                    status = workerStatus.Staus
                };

                dbContext.tblWorkerStatus.InsertOnSubmit(newWorkerStatus);

                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.IworkerStatus workerStatus)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var workerStatusUpdateing = dbContext.tblWorkerStatus.SingleOrDefault(
                    x => x.statusNo == workerStatus.StautsNo);

                workerStatusUpdateing.status = workerStatus.Staus;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
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

        public int NextId
        {
            get
            {
                string connString = string.Empty;
                using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
                {
                    connString = dbContext.Connection.ConnectionString;
                }

                var conn = new SqlClient.SqlConnection(connString);
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblWorkerStatus]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
