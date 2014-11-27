using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;
using System.Data.Linq;

namespace RDGs
{
    public class RDGtblDepartment
    {
        public List<Interface.Idepartment> Get(bool? active)
        {
            var list = new List<Interface.Idepartment>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                IQueryable<tblDepartment> dbContentDepartments;

                if (active == null)
                {
                    dbContentDepartments = from tblDepartment in dbContext.tblDepartments
                                           select tblDepartment;
                }
                else
                {
                    dbContentDepartments = from tblDepartment in dbContext.tblDepartments
                                           where tblDepartment.active == active
                                           select tblDepartment;
                }

                foreach (var item in dbContentDepartments)
                {
                    var department = new InterfaceAdaptor.Department();
                    
                    department.Active = (bool)item.active;
                    department.Address = item._address;
                    department.AltPhoneNo = item.altPhoneNo;
                    department.CompanyName = item.companyName;
                    department.CvrNo = item.cvrNo;
                    department.Deparment = item.department;
                    department.Email = item.email;
                    department.PhoneNo = item.phoneNo;
                    department.DeparmentHead = new InterfaceAdaptor.Worker()
                    {
                        Active = (bool)item.tblWorker.active,
                        Address = item.tblWorker.homeAddress,
                        AltPhoneNo = item.tblWorker.altPhoneNo,
                        Email = item.tblWorker.email,
                        Name = item.tblWorker.name,
                        PhoneNo = item.tblWorker.phoneNo,
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = item.tblWorker.tblPostNo.city,
                            Id = item.tblWorker.tblPostNo.ID,
                            PostNumber = item.tblWorker.tblPostNo.postNo
                        },
                        Surname = item.tblWorker.surname,
                        WorkerStatus = new InterfaceAdaptor.WorkerStatus()
                        {
                            Staus = item.tblWorker.tblWorkerStatus.status,
                            StautsNo = item.tblWorker.tblWorkerStatus.statusNo
                        },
                        WorkNo = item.tblWorker.workNo
                    };
                    department.PostNo = new InterfaceAdaptor.PostNo()
                    {
                        City = item.tblPostNo.city,
                        Id = item.tblPostNo.ID,
                        PostNumber = item.tblPostNo.postNo
                    };

                    list.Add(department);
                }
            }
            
            return list;
        }

        public Interface.Idepartment Find(int deparmentNo)
        {
            var department = new InterfaceAdaptor.Department();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var item = dbContext.tblDepartments.SingleOrDefault(x => x.department == deparmentNo);

                department.Active = (bool)item.active;
                department.Address = item._address;
                department.AltPhoneNo = item.altPhoneNo;
                department.CompanyName = item.companyName;
                department.CvrNo = item.cvrNo;
                department.Deparment = item.department;
                department.Email = item.email;
                department.PhoneNo = item.phoneNo;

                #region department => DeparmentHead : Worker
                department.DeparmentHead = new InterfaceAdaptor.Worker()
                {
                    Active = (bool)item.tblWorker.active,
                    Address = item.tblWorker.homeAddress,
                    AltPhoneNo = item.tblWorker.altPhoneNo,
                    Email = item.tblWorker.email,
                    Name = item.tblWorker.name,
                    PhoneNo = item.tblWorker.phoneNo,
                    Surname = item.tblWorker.surname,
                    WorkNo = item.tblWorker.workNo,

                    #region DeparmentHead => Post number
                    PostNo = new InterfaceAdaptor.PostNo()
                    {
                        City = item.tblWorker.tblPostNo.city,
                        Id = item.tblWorker.tblPostNo.ID,
                        PostNumber = item.tblWorker.tblPostNo.postNo
                    },
                    #endregion

                    #region DeparmentHead => Worker Status

                    WorkerStatus = new InterfaceAdaptor.WorkerStatus()
                    {
                        Staus = item.tblWorker.tblWorkerStatus.status,
                        StautsNo = item.tblWorker.tblWorkerStatus.statusNo
                    }
                    #endregion
                };
                #endregion

                //88
                department.PostNo = new InterfaceAdaptor.PostNo()
                {
                    City = item.tblPostNo.city,
                    Id = item.tblPostNo.ID,
                    PostNumber = item.tblPostNo.postNo
                };
            }

            return department;
        }

        public void Add(Interface.Idepartment department)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                tblDepartment newDepartment = new tblDepartment
                {
                    _address = department.Address,
                    active = department.Active,
                    altPhoneNo = department.AltPhoneNo,
                    companyName = department.CompanyName,
                    cvrNo = department.CvrNo,
                    department = department.Deparment,
                    departmentHeadNo = department.DeparmentHead.WorkNo,
                    email = department.Email,
                    phoneNo = department.PhoneNo,
                    postNo = department.PostNo.Id
                };

                dbContext.tblDepartments.InsertOnSubmit(newDepartment);
                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.Idepartment department)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                tblDepartment updateDepartment = dbContext.tblDepartments.SingleOrDefault(x => x.department == department.Deparment);

                updateDepartment._address = department.Address;
                updateDepartment.active = department.Active;
                updateDepartment.altPhoneNo = department.AltPhoneNo;
                updateDepartment.companyName = department.CompanyName;
                updateDepartment.cvrNo = department.CvrNo;
                updateDepartment.department = department.Deparment;
                updateDepartment.departmentHeadNo = department.DeparmentHead.WorkNo;
                updateDepartment.email = department.Email;
                updateDepartment.phoneNo = department.PhoneNo;
                updateDepartment.postNo = department.PostNo.Id;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(Interface.Idepartment department)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                tblDepartment item = dbContext.tblDepartments.SingleOrDefault(
                    x => x.department == department.Deparment);

                var itemString = new StringBuilder();
                itemString.Append("[tblDepartment] { ");
                itemString.Append("department = " + item.department.ToString() + ", ");
                itemString.Append("cvrNo = " + item.cvrNo.ToString() + ", ");
                itemString.Append("phoneNo = " + item.phoneNo + ", ");
                itemString.Append("altPhoneNo = " + item.altPhoneNo + ", ");
                itemString.Append("_address = " + item._address + ", ");
                itemString.Append("postNo = " + item.postNo.ToString() + ",");
                itemString.Append("email = " + item.email + ", ");
                
                if ((bool)item.active) itemString.Append("active = " + "1, "); 
                else itemString.Append("active = " + "1, ");
                
                itemString.Append("departmentHeadNo = " + item.departmentHeadNo.ToString() + " }");

                var deleteItem = new tblDeleteItem()
                {
                    itemInfo = itemString.ToString(),
                    deleteDate = DateTime.Now,
                    restored = false,
                };

                dbContext.tblDeleteItems.InsertOnSubmit(deleteItem);
                dbContext.tblDepartments.DeleteOnSubmit(item);

                dbContext.SubmitChanges();
            }
        }
    }
}
