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

        }

        public void Update(InterfaceAdaptor.Worker worker)
        {
            
        }

        public void Delete(InterfaceAdaptor.Worker worker)
        {

        }
    }
}
