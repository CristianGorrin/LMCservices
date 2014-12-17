using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

namespace RDGs
{
    public class RDGtblInvoicePrivet
    {
        private string connectionString;

        public RDGtblInvoicePrivet(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Interface.IinvoicePrivet> Get(bool? active)
        {
            var list = new List<Interface.IinvoicePrivet>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                IQueryable<tblInvoicePrivet> dbList;
                
                if (active == null)
                {
                    dbList = from tblInvoicePrivet in dbContext.tblInvoicePrivets
                             select tblInvoicePrivet;
                }
                else
                {
                    dbList = from tblInvoicePrivet in dbContext.tblInvoicePrivets
                             where tblInvoicePrivet.Active == (bool)active
                             select tblInvoicePrivet;
                }

                foreach (var item in dbList)
                {
                    list.Add(new InterfaceAdaptor.InvoicePrivet()
                    {
                        Active = item.Active,
                        Id = item.Id,
                        Order = new int?[] 
                        { 
                            item.OrderNr1,
                            item.OrderNr2,
                            item.OrderNr3,
                            item.OrderNr4,
                            item.OrderNr5,
                            item.OrderNr6,
                            item.OrderNr7,
                            item.OrderNr8,
                            item.OrderNr9,
                            item.OrderNr10,
                            item.OrderNr11,
                            item.OrderNr12,
                            item.OrderNr13,
                            item.OrderNr14,
                            item.OrderNr15,
                            item.OrderNr16,
                            item.OrderNr17,
                            item.OrderNr18,
                            item.OrderNr19,
                            item.OrderNr20,
                        }
                    });
                }
            }

            return list;
        }

        public Interface.IinvoicePrivet Find(int id)
        {
            InterfaceAdaptor.InvoicePrivet invoice = null;

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var found = dbContext.tblInvoicePrivets.SingleOrDefault(
                    x => x.Id == id);

                invoice = new InterfaceAdaptor.InvoicePrivet()
                {
                    Active = found.Active,
                    Id = found.Id,
                    Order = new int?[] 
                    { 
                        found.OrderNr1,
                        found.OrderNr2,
                        found.OrderNr3,
                        found.OrderNr4,
                        found.OrderNr5,
                        found.OrderNr6,
                        found.OrderNr7,
                        found.OrderNr8,
                        found.OrderNr9,
                        found.OrderNr10,
                        found.OrderNr11,
                        found.OrderNr12,
                        found.OrderNr13,
                        found.OrderNr14,
                        found.OrderNr15,
                        found.OrderNr16,
                        found.OrderNr17,
                        found.OrderNr18,
                        found.OrderNr19,
                        found.OrderNr20,
                    }
                };
            }

            return invoice;
        }

        public void Add(Interface.IinvoicePrivet obj)
        {
            using (LMCdatabaseDataContext dbCotext = new LMCdatabaseDataContext(this.connectionString))
            {
                var newInvoice = new tblInvoicePrivet()
                {
                    Active = obj.Active,
                    OrderNr1 = obj.Order[0],
                    OrderNr2 = obj.Order[1],
                    OrderNr3 = obj.Order[2],
                    OrderNr4 = obj.Order[3],
                    OrderNr5 = obj.Order[4],
                    OrderNr6 = obj.Order[5],
                    OrderNr7 = obj.Order[6],
                    OrderNr8 = obj.Order[7],
                    OrderNr9 = obj.Order[8],
                    OrderNr10 = obj.Order[9],
                    OrderNr11 = obj.Order[10],
                    OrderNr12 = obj.Order[11],
                    OrderNr13 = obj.Order[12],
                    OrderNr14 = obj.Order[13],
                    OrderNr15 = obj.Order[14],
                    OrderNr16 = obj.Order[15],
                    OrderNr17 = obj.Order[16],
                    OrderNr18 = obj.Order[17],
                    OrderNr19 = obj.Order[18],
                    OrderNr20 = obj.Order[19],
                };

                dbCotext.tblInvoicePrivets.InsertOnSubmit(newInvoice);
                dbCotext.SubmitChanges();
            }
        }

        public void Update(Interface.IinvoicePrivet obj)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var updating = dbContext.tblInvoicePrivets.SingleOrDefault(
                    x => x.Id == obj.Id);

                updating.Active = obj.Active;

                updating.OrderNr1 = obj.Order[0];
                updating.OrderNr2 = obj.Order[1];
                updating.OrderNr3 = obj.Order[2];
                updating.OrderNr4 = obj.Order[3];
                updating.OrderNr5 = obj.Order[4];
                updating.OrderNr6 = obj.Order[5];
                updating.OrderNr7 = obj.Order[6];
                updating.OrderNr8 = obj.Order[7];
                updating.OrderNr9 = obj.Order[8];
                updating.OrderNr10 = obj.Order[9];
                updating.OrderNr11 = obj.Order[10];
                updating.OrderNr12 = obj.Order[11];
                updating.OrderNr13 = obj.Order[12];
                updating.OrderNr14 = obj.Order[13];
                updating.OrderNr15 = obj.Order[14];
                updating.OrderNr16 = obj.Order[15];
                updating.OrderNr17 = obj.Order[16];
                updating.OrderNr18 = obj.Order[17];
                updating.OrderNr19 = obj.Order[18];
                updating.OrderNr20 = obj.Order[19];

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var deleteing = dbContext.tblInvoicePrivets.SingleOrDefault(
                    x => x.Id == id);

                var deleteInfo = new StringBuilder();
                deleteInfo.Append("[tblInvoicePrivet] { ");
                deleteInfo.Append("ID = " + deleteing.Id.ToString() + ", ");
                deleteInfo.Append("Active = ");
                if (deleteing.Active) { deleteInfo.Append("1"); } else { deleteInfo.Append("0"); }

                deleteInfo.Append("OrderNr1 = " + deleteing.OrderNr1.ToString() + ", ");
                deleteInfo.Append("OrderNr2 = " + deleteing.OrderNr2.ToString() + ", ");
                deleteInfo.Append("OrderNr3 = " + deleteing.OrderNr3.ToString() + ", ");
                deleteInfo.Append("OrderNr4 = " + deleteing.OrderNr4.ToString() + ", ");
                deleteInfo.Append("OrderNr5 = " + deleteing.OrderNr5.ToString() + ", ");
                deleteInfo.Append("OrderNr6 = " + deleteing.OrderNr6.ToString() + ", ");
                deleteInfo.Append("OrderNr7 = " + deleteing.OrderNr7.ToString() + ", ");
                deleteInfo.Append("OrderNr8 = " + deleteing.OrderNr8.ToString() + ", ");
                deleteInfo.Append("OrderNr9 = " + deleteing.OrderNr9.ToString() + ", ");
                deleteInfo.Append("OrderNr10 = " + deleteing.OrderNr10.ToString() + ", ");
                deleteInfo.Append("OrderNr11 = " + deleteing.OrderNr11.ToString() + ", ");
                deleteInfo.Append("OrderNr12 = " + deleteing.OrderNr12.ToString() + ", ");
                deleteInfo.Append("OrderNr13 = " + deleteing.OrderNr13.ToString() + ", ");
                deleteInfo.Append("OrderNr14 = " + deleteing.OrderNr14.ToString() + ", ");
                deleteInfo.Append("OrderNr15 = " + deleteing.OrderNr15.ToString() + ", ");
                deleteInfo.Append("OrderNr16 = " + deleteing.OrderNr16.ToString() + ", ");
                deleteInfo.Append("OrderNr17 = " + deleteing.OrderNr17.ToString() + ", ");
                deleteInfo.Append("OrderNr18 = " + deleteing.OrderNr18.ToString() + ", ");
                deleteInfo.Append("OrderNr19 = " + deleteing.OrderNr19.ToString() + ", ");
                deleteInfo.Append("OrderNr20 = " + deleteing.OrderNr20.ToString() + " }");

                dbContext.tblDeleteItems.InsertOnSubmit(new tblDeleteItem()
                {
                    deleteDate = DateTime.Now,
                    itemInfo = deleteInfo.ToString(),
                    restored = false
                });
                dbContext.tblInvoicePrivets.DeleteOnSubmit(deleteing);

                dbContext.SubmitChanges();
            }
        }

        public int[] OrdersInUse(int orderId)
        {
            int[] inUse = null;
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                IQueryable<tblInvoicePrivet> dbList;

                dbList = from tblInvoicePrivet in dbContext.tblInvoicePrivets
                         where 
                         tblInvoicePrivet.OrderNr1 == orderId || tblInvoicePrivet.OrderNr2 == orderId ||
                         tblInvoicePrivet.OrderNr3 == orderId || tblInvoicePrivet.OrderNr4 == orderId ||
                         tblInvoicePrivet.OrderNr5 == orderId || tblInvoicePrivet.OrderNr6 == orderId ||
                         tblInvoicePrivet.OrderNr7 == orderId || tblInvoicePrivet.OrderNr8 == orderId ||
                         tblInvoicePrivet.OrderNr9 == orderId || tblInvoicePrivet.OrderNr10 == orderId ||
                         tblInvoicePrivet.OrderNr11 == orderId || tblInvoicePrivet.OrderNr12 == orderId ||
                         tblInvoicePrivet.OrderNr13 == orderId || tblInvoicePrivet.OrderNr14 == orderId ||
                         tblInvoicePrivet.OrderNr15 == orderId || tblInvoicePrivet.OrderNr16 == orderId ||
                         tblInvoicePrivet.OrderNr17 == orderId || tblInvoicePrivet.OrderNr18 == orderId ||
                         tblInvoicePrivet.OrderNr19 == orderId || tblInvoicePrivet.OrderNr20 == orderId
                         select tblInvoicePrivet;

                var list = new List<int>();

                foreach (var item in dbList)
                {
                    list.Add(item.Id);
                }

                inUse = list.ToArray<int>();
            }

            return inUse;
        }

        public bool UpdateActive(int id, bool active)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var item = dbContext.tblInvoicePrivets.SingleOrDefault(
                    x => x.Id == id);

                if (item == null)
                    return false;

                item.Active = active;

                dbContext.SubmitChanges();
            }

            return true;
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
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblInvoicePrivet]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
