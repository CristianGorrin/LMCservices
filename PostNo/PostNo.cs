using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace PostNo
{
    public class PostNo : Interface.IpostNo
    {
        private string city;
        private int id;
        private int postNo;

        public PostNo(string city, int id, int postNo)
        {
            this.city = city;
            this.id = id;
            this.postNo = postNo;
        }

        public PostNo(PostNo obj)
        {
            this.city = obj.city;
            this.id = obj.id;
            this.postNo = obj.postNo;
        }

        public PostNo(int id)
        {
            this.city = string.Empty;
            this.id = id;
            this.postNo = -1;
        }

        public string City { get { return this.city; } set { this.city = value; } }
        public int Id { get { return this.id; } } // is reed only used in DB
        public int PostNumber { get { return this.postNo; } set { this.postNo = value; } }
    }
}
