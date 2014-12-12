using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using API;

namespace PostNo
{
    public class PostNumbers : API.Lists<Interface.IpostNo>
    {
        public PostNumbers()
            : base()
        {
        }

        public override System.Data.DataTable AsDataTable()
        {
            var dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("City", typeof(string));
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Post Number", typeof(int));

            foreach (var item in this.list)
            {
                dataTable.Rows.Add(
                    item.City,
                    item.Id,
                    item.PostNumber);
            }

            return dataTable;
        }



        public bool Validate(int number)
        {
            foreach (var item in this.list)
            {
                if (item.PostNumber == number)
                {
                    return true;
                }
            }

            return false;
        }

        public Interface.IpostNo GetAtPostNumber(int number)
        {
            foreach (var item in this.list)
            {
                if (item.PostNumber == number)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
