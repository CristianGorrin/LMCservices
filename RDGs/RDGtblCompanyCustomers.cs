using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

namespace RDGs
{
    public class RDGtblCompanyCustomers
    {
        private string connectionString;

        public RDGtblCompanyCustomers(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Interface.IcompanyCustomer> Get(bool? active)
        {
            var list = new List<Interface.IcompanyCustomer>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                IQueryable<tblCompanyCustomer> companyCustomers;

                if (active == null)
                {
                    companyCustomers = from tblCompanyCustomer in dbContext.tblCompanyCustomers
                                       select tblCompanyCustomer;
                }
                else
                {
                    companyCustomers = from tblCompanyCustomer in dbContext.tblCompanyCustomers
                                       where tblCompanyCustomer.active == active
                                       select tblCompanyCustomer;
                }

                foreach (var item in companyCustomers)
                {
                    list.Add(new InterfaceAdaptor.CompanyCustomer()
                    {
                        Active = (bool)item.active,
                        Address = item._address,
                        AltPhoneNo = item.altPhoneNo,
                        CompanyCustomersNo = item.companyCustomersNo,
                        ContactPerson = item.companyContactPerson,
                        CvrNo = item.cvrNo,
                        Email = item.email,
                        Name = item.companyName,
                        PhoneNo = item.phoneNo,
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = item.tblPostNo.city,
                            Id = item.tblPostNo.ID,
                            PostNumber = item.tblPostNo.postNo
                        }
                    });
                }
            }

            return list;
        }

        public Interface.IcompanyCustomer Find(int id)
        {
            Interface.IcompanyCustomer companyCustomer;

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var companyCustomerFound = dbContext.tblCompanyCustomers.SingleOrDefault(
                    x => x.companyCustomersNo == id);

                companyCustomer = new InterfaceAdaptor.CompanyCustomer()
                {
                    Active = (bool)companyCustomerFound.active,
                    Address = companyCustomerFound._address,
                    AltPhoneNo = companyCustomerFound.altPhoneNo,
                    CompanyCustomersNo = companyCustomerFound.companyCustomersNo,
                    ContactPerson = companyCustomerFound.companyContactPerson,
                    CvrNo = companyCustomerFound.cvrNo,
                    Email = companyCustomerFound.email,
                    Name = companyCustomerFound.companyName,
                    PhoneNo = companyCustomerFound.phoneNo,
                    PostNo = new InterfaceAdaptor.PostNo()
                    {
                        City = companyCustomerFound.tblPostNo.city,
                        Id = companyCustomerFound.tblPostNo.ID,
                        PostNumber = companyCustomerFound.tblPostNo.postNo
                    },
                };
            }

            return companyCustomer;
        }

        public void Add(Interface.IcompanyCustomer companyCustomer)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var newCompanyCustomer = new tblCompanyCustomer()
                {
                    _address = companyCustomer.Address,
                    active = companyCustomer.Active,
                    altPhoneNo = companyCustomer.AltPhoneNo,
                    companyContactPerson = companyCustomer.ContactPerson,
                    companyCustomersNo = companyCustomer.CompanyCustomersNo,
                    companyName = companyCustomer.Name,
                    cvrNo = companyCustomer.CvrNo,
                    email = companyCustomer.Email,
                    phoneNo = companyCustomer.PhoneNo,
                    postNo = companyCustomer.PostNo.Id,
                };

                dbContext.tblCompanyCustomers.InsertOnSubmit(newCompanyCustomer);
                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.IcompanyCustomer companyCustomer)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var companyCustomerUpdateing = dbContext.tblCompanyCustomers.SingleOrDefault(
                    x => x.companyCustomersNo == companyCustomer.CompanyCustomersNo);
                if (companyCustomer.Address != string.Empty)
                {
                    companyCustomerUpdateing._address = companyCustomer.Address;
                }
                
                companyCustomerUpdateing.active = companyCustomer.Active;

                companyCustomerUpdateing.altPhoneNo = companyCustomer.AltPhoneNo;

                if (companyCustomer.ContactPerson != string.Empty)
                {
                    companyCustomerUpdateing.companyContactPerson = companyCustomer.ContactPerson;
                }

                if (companyCustomer.Name != string.Empty)
                {
                    companyCustomerUpdateing.companyName = companyCustomer.Name;
                }

                if (companyCustomer.CvrNo != -1)
                {
                    companyCustomerUpdateing.cvrNo = companyCustomer.CvrNo;
                }

                companyCustomerUpdateing.email = companyCustomer.Email;

                if (companyCustomer.PhoneNo != string.Empty)
                {
                    companyCustomerUpdateing.phoneNo = companyCustomer.PhoneNo;
                }

                if (companyCustomer.PostNo.Id != -1)
                {
                    companyCustomerUpdateing.postNo = companyCustomer.PostNo.Id;
                }

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var deletingItem = dbContext.tblCompanyCustomers.SingleOrDefault(
                    x => x.companyCustomersNo == id);

                var deletingInfo = new StringBuilder();
                deletingInfo.Append("[tblCompanyCustomers] { ");
                deletingInfo.Append("companyCustomersNo = " + deletingItem.companyCustomersNo.ToString() + ", ");
                deletingInfo.Append("companyName = " + deletingItem.companyName + ", ");
                deletingInfo.Append("companyContactPerson = " + deletingItem.companyContactPerson + ", ");
                deletingInfo.Append("cvrNo = " + deletingItem.cvrNo.ToString() + ", ");
                deletingInfo.Append("phoneNo = " + deletingItem.phoneNo + ", ");
                deletingInfo.Append("altPhoneNo = " + deletingItem.altPhoneNo + ", ");
                deletingInfo.Append("_address = " + deletingItem._address + ", ");
                deletingInfo.Append("postNo = " + deletingItem.postNo.ToString() + ", ");
                deletingInfo.Append("email = " + deletingItem.email + ", ");
                deletingInfo.Append("active = ");
                if ((bool)deletingItem.active) { deletingInfo.Append("1"); } else { deletingInfo.Append("0"); }
                deletingInfo.Append(" }");

                deletingItem.active = false;

                dbContext.tblDeleteItems.InsertOnSubmit(new tblDeleteItem()
                {
                    deleteDate = DateTime.Now,
                    itemInfo = deletingInfo.ToString(),
                    restored = false
                });

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
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblCompanyCustomers]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
