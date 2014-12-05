using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using API;

namespace Company
{
    public class Departments : API.Lists<Interface.Idepartment>
    {
        public Departments()
            : base()
        {
        }

        public override System.Data.DataTable AsDataTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(@"ID", typeof(int));
            dataTable.Columns.Add(@"Company Name", typeof(string));
            dataTable.Columns.Add(@"CVR Number", typeof(int));
            dataTable.Columns.Add(@"Department Head", typeof(string));
            dataTable.Columns.Add(@"Address", typeof(string));
            dataTable.Columns.Add(@"Post Number", typeof(int));
            dataTable.Columns.Add(@"City", typeof(string));
            dataTable.Columns.Add(@"Phone Number", typeof(string));
            dataTable.Columns.Add(@"Alt Phone Number", typeof(string));
            dataTable.Columns.Add(@"Email", typeof(string));

            foreach (var item in this.list)
            {
                   dataTable.Rows.Add(
                       item.Deparment,
                       item.CompanyName,
                       item.CvrNo,
                       @"#" + item.DeparmentHead.WorkNo.ToString() + " - " + item.DeparmentHead.Name,
                       item.Address,
                       item.PostNo.PostNumber,
                       item.PostNo.City,
                       item.PhoneNo,
                       item.AltPhoneNo,
                       item.Email);
            }

            return dataTable;
        }
    }

    public class Workers : API.Lists<Interface.Iworker>
    {
        public Workers()
            : base()
        {
        }

        public override System.Data.DataTable AsDataTable()
        {
            var dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("Active", typeof(bool));
            dataTable.Columns.Add("Address", typeof(string));
            dataTable.Columns.Add("Alt Phone Number", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Phone Number", typeof(string));
            dataTable.Columns.Add("Post Number", typeof(int));
            dataTable.Columns.Add("City", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));

            foreach (var item in this.list)
            {
                dataTable.Rows.Add(
                    item.Active,
                    item.Address,
                    item.AltPhoneNo,
                    item.Email,
                    item.Name,
                    item.PhoneNo,
                    item.PostNo.PostNumber,
                    item.PostNo.City,
                    item.WorkerStatus.Staus,
                    item.WorkNo);
            }

            return dataTable;
        }
    }
}
