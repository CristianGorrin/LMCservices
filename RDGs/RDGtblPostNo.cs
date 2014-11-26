using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace RDGs
{
    public class RDGtblPostNo
    {
        public List<Interface.IpostNo> Get()
        {
            List<Interface.IpostNo> list = new List<Interface.IpostNo>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var dbContent = dbContext.tblPostNos;

                foreach (var item in dbContent)
                {
                    var PostNo = new PostNo();
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
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
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
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                tblPostNo postNo = dbContext.tblPostNos.SingleOrDefault(x => x.ID == postNumber.Id);
                postNo.city = postNumber.City;
                postNo.postNo = postNumber.PostNumber;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(Interface.IpostNo postNumber)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
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
            var postNumber = new PostNo();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var postNumbersTbl = dbContext.tblPostNos;

                var postNo = postNumbersTbl.SingleOrDefault(x => x.ID == id);

                postNumber.City = postNo.city;
                postNumber.Id = postNo.ID;
                postNumber.PostNumber = postNo.postNo;
            }

            return postNumber;
        }

        class PostNo : Interface.IpostNo
        {
            public string City { get; set; }
            public int Id { get; set; }
            public int PostNumber { get; set; }
        }
    }
}
