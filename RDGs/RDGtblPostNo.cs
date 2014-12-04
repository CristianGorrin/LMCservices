using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

using Interface;

namespace RDGs
{
    public class RDGtblPostNo
    {
        private string connectionString;

        public RDGtblPostNo(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<Interface.IpostNo> Get()
        {
            List<Interface.IpostNo> list = new List<Interface.IpostNo>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var dbContent = dbContext.tblPostNos;

                foreach (var item in dbContent)
                {
                    var PostNo = new InterfaceAdaptor.PostNo();
                    PostNo.Id = item.ID;
                    PostNo.City = item.city;
                    PostNo.PostNumber = item.postNo;

                    list.Add(PostNo);
                }
            }

            return list;
        }

        public void Add(Interface.IpostNo postNumber)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                tblPostNo newPostNumber = new tblPostNo
                {
                    postNo = postNumber.PostNumber,
                    city = postNumber.City
                };

                dbContext.tblPostNos.InsertOnSubmit(newPostNumber);
                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.IpostNo postNumber)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                tblPostNo postNo = dbContext.tblPostNos.SingleOrDefault(x => x.ID == postNumber.Id);
                postNo.city = postNumber.City;
                postNo.postNo = postNumber.PostNumber;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(Interface.IpostNo postNumber)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                tblPostNo postNo = dbContext.tblPostNos.SingleOrDefault(x => x.ID == postNumber.Id);
                dbContext.tblPostNos.DeleteOnSubmit(postNo);

                var deleteItem = new StringBuilder();
                deleteItem.Append("[tblPostNo] { ");
                deleteItem.Append("ID = " + postNo.ID + ", ");
                deleteItem.Append("postNo = " + postNo.postNo + ", ");
                deleteItem.Append("city = " + postNo.city);
                deleteItem.Append("} ");

                tblDeleteItem newDelete = new tblDeleteItem
                {
                    itemInfo = deleteItem.ToString(),
                    deleteDate = DateTime.Now,
                    restored = false
                };

                dbContext.SubmitChanges();
            }
        }

        public Interface.IpostNo Find(int id)
        {
            var postNumber = new InterfaceAdaptor.PostNo();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var postNumbersTbl = dbContext.tblPostNos;

                var postNo = postNumbersTbl.SingleOrDefault(x => x.ID == id);

                postNumber.City = postNo.city;
                postNumber.Id = postNo.ID;
                postNumber.PostNumber = postNo.postNo;
            }

            return postNumber;
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
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblPostNo]')", conn);
                
                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
