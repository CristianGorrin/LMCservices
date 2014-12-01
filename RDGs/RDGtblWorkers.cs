using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDGs
{
    public class RDGtblWorkers
    {
        public List<InterfaceAdaptor.Worker> Get(bool? active)
        {
            var list = new List<InterfaceAdaptor.Worker>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                IQueryable<tblWorker> workers;

                if (active == null)
                {
                    workers = from tblWorker in dbContext.tblWorkers
                              select tblWorker;
                }
                else
                {
                    workers = from tblWorker in dbContext.tblWorkers
                              where tblWorker.active == active
                              select tblWorker;
                }

                foreach (var item in workers)
                {
                    #region worker
                    var worker = new InterfaceAdaptor.Worker()
                    {
                        Active = (bool)item.active,
                        Address = item.homeAddress,
                        AltPhoneNo = item.altPhoneNo,
                        Email = item.email,
                        Name = item.name,
                        PhoneNo = item.phoneNo,
                        #region Post number => worker
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = item.tblPostNo.city,
                            Id = item.tblPostNo.ID,
                            PostNumber = item.tblPostNo.postNo
                        },
                        #endregion
                        Surname = item.surname,
                        #region Worker Status => worker
                        WorkerStatus = new InterfaceAdaptor.WorkerStatus()
                        {
                            Staus = item.tblWorkerStatus.status,
                            StautsNo = item.tblWorkerStatus.statusNo
                        },
                        #endregion
                        WorkNo = item.workNo
                    };
                    #endregion

                    list.Add(worker);
                }
            }

            return list;
        }

        public InterfaceAdaptor.Worker Find(int workerNumber)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var item = dbContext.tblWorkers.Single(x => x.workNo == workerNumber);

                #region worker
                var worker = new InterfaceAdaptor.Worker()
                {
                    Active = (bool)item.active,
                    Address = item.homeAddress,
                    AltPhoneNo = item.altPhoneNo,
                    Email = item.email,
                    Name = item.name,
                    PhoneNo = item.phoneNo,
                    #region Post Number => worker
                    PostNo = new InterfaceAdaptor.PostNo()
                    {
                        City = item.tblPostNo.city,
                        Id = item.tblPostNo.ID,
                        PostNumber = item.tblPostNo.postNo
                    },
                    #endregion
                    Surname = item.surname,
                    #region Worker Status => Worker
                    WorkerStatus = new InterfaceAdaptor.WorkerStatus()
                    {
                        Staus = item.tblWorkerStatus.status,
                        StautsNo = item.tblWorkerStatus.statusNo
                    },
                    #endregion
                    WorkNo = item.workNo
                };
                #endregion

                return worker;
            }
        }

        public void Add(InterfaceAdaptor.Worker worker)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var newWorker = new tblWorker()
                {
                    active = worker.Active,
                    altPhoneNo = worker.AltPhoneNo,
                    email = worker.Email,
                    homeAddress = worker.Address,
                    name = worker.Name,
                    phoneNo = worker.PhoneNo,
                    postNo = worker.PostNo.Id,
                    surname = worker.Surname,
                    workerStatus = worker.WorkerStatus.StautsNo,
                };

                dbContext.tblWorkers.InsertOnSubmit(newWorker);
                dbContext.SubmitChanges();
            }
        }

        public void Update(InterfaceAdaptor.Worker worker)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                tblWorker updateWorker = dbContext.tblWorkers.SingleOrDefault(x => x.workNo == worker.WorkNo);

                updateWorker.active = worker.Active;
                updateWorker.altPhoneNo = worker.AltPhoneNo;
                updateWorker.email = worker.Email;
                updateWorker.homeAddress = worker.Address;
                updateWorker.name = worker.Name;
                updateWorker.phoneNo = worker.PhoneNo;
                updateWorker.postNo = worker.PostNo.Id;
                updateWorker.surname = worker.Surname;
                updateWorker.workerStatus = worker.WorkerStatus.StautsNo;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int WorkNumber)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                tblWorker workerItem = dbContext.tblWorkers.SingleOrDefault(x => x.workNo == WorkNumber);

                var workerDelete = new StringBuilder();
                workerDelete.Append("[tblWorkers] { ");
                workerDelete.Append("workNo = " + workerItem.workNo.ToString() + ", ");
                workerDelete.Append("name = " + workerItem.name + ", ");
                workerDelete.Append("surname = " + workerItem.surname + ", ");
                workerDelete.Append("workerStatus = " + workerItem.workerStatus.ToString() + ", ");
                workerDelete.Append("phoneNo = " + workerItem.phoneNo + ", ");
                workerDelete.Append("altPhoneNo = " + workerItem.altPhoneNo + ", ");
                workerDelete.Append("homeAddress = " + workerItem.homeAddress + ", ");
                workerDelete.Append("postNo = " + workerItem.postNo.ToString() + ", ");
                workerDelete.Append("email = " + workerItem.email + ", ");
                workerDelete.Append("active = ");
                if (workerItem.active == true) { workerDelete.Append("1"); } else { workerDelete.Append("0"); }
                workerDelete.Append(" }");

                var newDeleteItem = new tblDeleteItem()
                {
                    deleteDate = DateTime.Now,
                    itemInfo = workerDelete.ToString(),
                    restored = false
                };

                dbContext.tblDeleteItems.InsertOnSubmit(newDeleteItem);
                dbContext.tblWorkers.DeleteOnSubmit(workerItem);

                dbContext.SubmitChanges();
            }
        }

        public int NextId
        {
            get
            {
                int id;

                using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
                {
                    id = dbContext.tblWorkers.Max(x => x.workNo);
                    id++;
                }

                return id;
            }
        }
    }
}
