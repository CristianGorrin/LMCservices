using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

namespace RDGs
{
    public class RDGtblPrivateCustomers
    {
        private string connectionString;

        public RDGtblPrivateCustomers(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Interface.IprivetCustomer> Get(bool? active)
        {
            var list = new List<Interface.IprivetCustomer>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                IQueryable<tblPrivateCustomer> tblPrivateCustomers;

                if (active == null)
                {
                    tblPrivateCustomers = from tblPrivateCustomer in dbContext.tblPrivateCustomers
                                          select tblPrivateCustomer;
                }
                else
                {
                    tblPrivateCustomers = from tblPrivateCustomer in dbContext.tblPrivateCustomers
                                          where tblPrivateCustomer.active == active
                                          select tblPrivateCustomer;
                }

                foreach (var item in tblPrivateCustomers)
                {
                    var PrivateCustomer = new InterfaceAdaptor.PrivetCustomer()
                    {
                        Active = (bool)item.active,
                        AltPhoneNo = item.altPhoneNo,
                        Email = item.email,
                        HomeAddress = item.homeAddress,
                        Name = item.name,
                        PhoneNo = item.phoneNo,
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = item.tblPostNo.city,
                            Id = item.tblPostNo.ID,
                            PostNumber = item.tblPostNo.postNo
                        },
                        PrivateCustomersNo = item.privateCustomersNo,
                        Surname = item.surname,
                    };

                    list.Add(PrivateCustomer);
                }
            }

            return list;
        }

        public Interface.IprivetCustomer Find(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var privetCustomer = dbContext.tblPrivateCustomers.SingleOrDefault(
                    x => x.privateCustomersNo == id);

                return new InterfaceAdaptor.PrivetCustomer()
                {
                    Active = (bool)privetCustomer.active,
                    AltPhoneNo = privetCustomer.altPhoneNo,
                    Email = privetCustomer.email,
                    HomeAddress = privetCustomer.homeAddress,
                    Name = privetCustomer.name,
                    PhoneNo = privetCustomer.phoneNo,
                    PostNo = new InterfaceAdaptor.PostNo()
                    {
                        City = privetCustomer.tblPostNo.city,
                        Id = privetCustomer.tblPostNo.ID,
                        PostNumber = privetCustomer.tblPostNo.postNo
                    },
                    PrivateCustomersNo = privetCustomer.privateCustomersNo,
                    Surname = privetCustomer.surname,
                };
            }
        }

        public void Add(Interface.IprivetCustomer privetCustomer)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                tblPrivateCustomer newPrivateCustomer = new tblPrivateCustomer()
                {
                    active = privetCustomer.Active,
                    altPhoneNo = privetCustomer.AltPhoneNo,
                    email = privetCustomer.Email,
                    homeAddress = privetCustomer.HomeAddress,
                    name = privetCustomer.Name,
                    postNo = privetCustomer.PostNo.Id,
                    phoneNo = privetCustomer.PhoneNo,
                    surname = privetCustomer.Surname,
                };

                dbContext.tblPrivateCustomers.InsertOnSubmit(newPrivateCustomer);

                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.IprivetCustomer privetCustomer)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var privetCustomerUpdateing = dbContext.tblPrivateCustomers.SingleOrDefault(
                    x => x.privateCustomersNo == privetCustomer.PrivateCustomersNo);

                privetCustomerUpdateing.active = privetCustomer.Active;
                privetCustomerUpdateing.altPhoneNo = privetCustomer.AltPhoneNo;
                privetCustomerUpdateing.email = privetCustomer.Email;
                privetCustomerUpdateing.homeAddress = privetCustomer.HomeAddress;
                privetCustomerUpdateing.name = privetCustomer.Name;
                privetCustomerUpdateing.phoneNo = privetCustomer.PhoneNo;
                privetCustomerUpdateing.postNo = privetCustomer.PostNo.Id;
                privetCustomerUpdateing.surname = privetCustomer.Surname;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var deleteingItem = dbContext.tblPrivateCustomers.SingleOrDefault(
                    x => x.privateCustomersNo == id);

                var deleteingInfo = new StringBuilder();
                deleteingInfo.Append("[tblPrivateCustomers] { ");
                deleteingInfo.Append("privateCustomersNo = " + deleteingItem.privateCustomersNo.ToString() + ", ");
                deleteingInfo.Append("name = " + deleteingItem.name + ", ");
                deleteingInfo.Append("surname = " + deleteingItem.surname + ", ");
                deleteingInfo.Append("phoneNo = " + deleteingItem.phoneNo + ", ");
                deleteingInfo.Append("altPhoneNo = " + deleteingItem.altPhoneNo + ", ");
                deleteingInfo.Append("homeAddress = " + deleteingItem.homeAddress + ", ");
                deleteingInfo.Append("postNo = " + deleteingItem.postNo.ToString() + ", ");
                deleteingInfo.Append("email = " + deleteingItem.email + ", ");
                deleteingInfo.Append("active = ");
                if (deleteingItem.active == true) { deleteingInfo.Append("1"); } else { deleteingInfo.Append("0"); }
                deleteingInfo.Append(" }");

                var deleteData = new tblDeleteItem()
                {
                    deleteDate = DateTime.Now,
                    itemInfo = deleteingItem.ToString(),
                    restored = false
                };

                dbContext.tblDeleteItems.InsertOnSubmit(deleteData);
                dbContext.tblPrivateCustomers.DeleteOnSubmit(deleteingItem);

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
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblPrivateCustomers]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
