using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

namespace RDGs
{
    public class RDGtblInvoiceCompany
    {
        private string connectionString;

        public RDGtblInvoiceCompany(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Interface.IinvoiceCompany> Get(bool? active)
        {
            var list = new List<Interface.IinvoiceCompany>();

            using(LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
	        {
                IQueryable<tblInvoiceCompany> dbList;
		        
                if (active == null)
	            {
                    dbList = from tblInvoiceCompany in dbContext.tblInvoiceCompanies
                             select tblInvoiceCompany;
                }
                else
                {
                    dbList = from tblInvoiceCompany in dbContext.tblInvoiceCompanies
                             where tblInvoiceCompany.Active == (bool)active
                             select tblInvoiceCompany;
                }

                InterfaceAdaptor.InvoiceCompany tempInvoice;

                foreach (var item in dbList)
                {
                    tempInvoice = new InterfaceAdaptor.InvoiceCompany();

                    tempInvoice.Active = item.Active;
                    tempInvoice.Id = item.Id;

                    tempInvoice.Order = new int?[20];
                    tempInvoice.Order[0] = item.OrderNr1;
                    tempInvoice.Order[1] = item.OrderNr2;
                    tempInvoice.Order[2] = item.OrderNr3;
                    tempInvoice.Order[3] = item.OrderNr4;
                    tempInvoice.Order[4] = item.OrderNr5;
                    tempInvoice.Order[5] = item.OrderNr6;
                    tempInvoice.Order[6] = item.OrderNr7;
                    tempInvoice.Order[7] = item.OrderNr8;
                    tempInvoice.Order[8] = item.OrderNr9;
                    tempInvoice.Order[9] = item.OrderNr10;
                    tempInvoice.Order[10] = item.OrderNr11;
                    tempInvoice.Order[11] = item.OrderNr12;
                    tempInvoice.Order[12] = item.OrderNr13;
                    tempInvoice.Order[13] = item.OrderNr14;
                    tempInvoice.Order[14] = item.OrderNr15;
                    tempInvoice.Order[15] = item.OrderNr16;
                    tempInvoice.Order[16] = item.OrderNr17;
                    tempInvoice.Order[17] = item.OrderNr18;
                    tempInvoice.Order[18] = item.OrderNr19;
                    tempInvoice.Order[19] = item.OrderNr20;

                    list.Add(tempInvoice);
                }
	        }

            return list;
        }

        public Interface.IinvoiceCompany Find(int id)
        {
            InterfaceAdaptor.InvoiceCompany invoice = new InterfaceAdaptor.InvoiceCompany();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var foundInvoice = dbContext.tblInvoiceCompanies.SingleOrDefault(
                    x => x.Id == id);


                invoice.Active = foundInvoice.Active;
                invoice.Id = foundInvoice.Id;
                
                invoice.Order = new int?[20];
                invoice.Order[0] = foundInvoice.OrderNr1;
                invoice.Order[1] = foundInvoice.OrderNr2;
                invoice.Order[2] = foundInvoice.OrderNr3;
                invoice.Order[3] = foundInvoice.OrderNr4;
                invoice.Order[4] = foundInvoice.OrderNr5;
                invoice.Order[5] = foundInvoice.OrderNr6;
                invoice.Order[6] = foundInvoice.OrderNr7;
                invoice.Order[7] = foundInvoice.OrderNr8;
                invoice.Order[8] = foundInvoice.OrderNr9;
                invoice.Order[9] = foundInvoice.OrderNr10;
                invoice.Order[10] = foundInvoice.OrderNr11;
                invoice.Order[11] = foundInvoice.OrderNr12;
                invoice.Order[12] = foundInvoice.OrderNr13;
                invoice.Order[13] = foundInvoice.OrderNr14;
                invoice.Order[14] = foundInvoice.OrderNr15;
                invoice.Order[15] = foundInvoice.OrderNr16;
                invoice.Order[16] = foundInvoice.OrderNr17;
                invoice.Order[17] = foundInvoice.OrderNr18;
                invoice.Order[18] = foundInvoice.OrderNr19;
                invoice.Order[19] = foundInvoice.OrderNr20;
            }

            return invoice;
        }

        public void Add(Interface.IinvoiceCompany obj)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var newInvoice = new tblInvoiceCompany()
                {
                    Active = obj.Active,
                    Id = obj.Id,
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

                dbContext.tblInvoiceCompanies.InsertOnSubmit(newInvoice);
                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.IinvoiceCompany obj)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var updateing = dbContext.tblInvoiceCompanies.SingleOrDefault(
                    x => x.Id == obj.Id);

                updateing.Active = obj.Active;

                updateing.OrderNr1 = obj.Order[0];
                updateing.OrderNr2 = obj.Order[1];
                updateing.OrderNr3 = obj.Order[2];
                updateing.OrderNr4 = obj.Order[3];
                updateing.OrderNr5 = obj.Order[4];
                updateing.OrderNr6 = obj.Order[5];
                updateing.OrderNr7 = obj.Order[6];
                updateing.OrderNr8 = obj.Order[7];
                updateing.OrderNr9 = obj.Order[8];
                updateing.OrderNr10 = obj.Order[9];
                updateing.OrderNr11 = obj.Order[10];
                updateing.OrderNr12 = obj.Order[11];
                updateing.OrderNr13 = obj.Order[12];
                updateing.OrderNr14 = obj.Order[13];
                updateing.OrderNr15 = obj.Order[14];
                updateing.OrderNr16 = obj.Order[15];
                updateing.OrderNr17 = obj.Order[16];
                updateing.OrderNr18 = obj.Order[17];
                updateing.OrderNr19 = obj.Order[18];
                updateing.OrderNr20 = obj.Order[19];

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var deleteing = dbContext.tblInvoiceCompanies.SingleOrDefault(
                    x => x.Id == id);

                var deleteInfo = new StringBuilder();
                deleteInfo.Append("[tblInvoiceCompany] { ");
                deleteInfo.Append("ID = " + deleteing.Id.ToString() + ", ");
                deleteInfo.Append("Active = ");
                if (deleteing.Active) { deleteInfo.Append("1"); } else { deleteInfo.Append("1"); }
                deleteInfo.Append(", ");
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
                dbContext.tblInvoiceCompanies.DeleteOnSubmit(deleteing);

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
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblInvoiceCompany]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }

        public bool UpdateActive(int id, bool active)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var item = dbContext.tblInvoiceCompanies.SingleOrDefault(
                    x => x.Id == id);

                if (item == null)
                    return false;

                item.Active = active;

                dbContext.SubmitChanges();
            }

            return true;
        }

        public int[] OrdersInUse(int id)
        {
            List<int> list = new List<int>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                IQueryable<tblInvoiceCompany> dbList;

                dbList = from tblInvoiceCompany in dbContext.tblInvoiceCompanies
                         where
                         tblInvoiceCompany.OrderNr1 == id || tblInvoiceCompany.OrderNr2 == id ||
                         tblInvoiceCompany.OrderNr3 == id || tblInvoiceCompany.OrderNr4 == id ||
                         tblInvoiceCompany.OrderNr5 == id || tblInvoiceCompany.OrderNr6 == id ||
                         tblInvoiceCompany.OrderNr7 == id || tblInvoiceCompany.OrderNr8 == id ||
                         tblInvoiceCompany.OrderNr9 == id || tblInvoiceCompany.OrderNr10 == id ||
                         tblInvoiceCompany.OrderNr11 == id || tblInvoiceCompany.OrderNr12 == id ||
                         tblInvoiceCompany.OrderNr13 == id || tblInvoiceCompany.OrderNr14 == id ||
                         tblInvoiceCompany.OrderNr15 == id || tblInvoiceCompany.OrderNr16 == id ||
                         tblInvoiceCompany.OrderNr17 == id || tblInvoiceCompany.OrderNr18 == id ||
                         tblInvoiceCompany.OrderNr19 == id || tblInvoiceCompany.OrderNr20 == id
                         select tblInvoiceCompany;

                foreach (var item in dbList)
                {
                    list.Add(item.Id);
                }
            }

            return list.ToArray<int>();
        }
    }
}
