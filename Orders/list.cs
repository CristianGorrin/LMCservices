using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using API;

namespace Orders
{
    public class CompanyOrders : API.Lists<Interface.IcompanyOrder>
    {
        public CompanyOrders()
            :base()
        {
        }

        public override System.Data.DataTable AsDataTable()
        {
            var dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("Create By", typeof(string));
            dataTable.Columns.Add("Create Date", typeof(DateTime));
            dataTable.Columns.Add("Customer", typeof(string));
            dataTable.Columns.Add("Send Bill Date", typeof(DateTime));
            dataTable.Columns.Add("Days to Pay", typeof(int));
            dataTable.Columns.Add("Task Description", typeof(string));
            dataTable.Columns.Add("Hours Use", typeof(double));
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Paid", typeof(bool));
            dataTable.Columns.Add("Paid Hour", typeof(double));
            dataTable.Columns.Add("Paid To Acc", typeof(int));
            dataTable.Columns.Add("Task Date", typeof(DateTime));

            foreach (var item in this.list)
            {
                dataTable.Rows.Add(
                    item.CreateBy.Name + " " + item.CreateBy.Surname,
                    item.CreateDate,
                    @"#" + item.Customer.CompanyCustomersNo.ToString() + " - " + item.Customer.Name,
                    item.DateSendBill,
                    item.DaysToPaid,
                    item.DescriptionTask,
                    item.HoutsUse,
                    item.InvoiceNo,
                    item.Paid,
                    item.PaidHour,
                    item.PaidToAcc,
                    item.TaskDate
                    );
            }

            return dataTable;
        }
    }

    public class PrivetOrders : API.Lists<Interface.IprivetOrder>
    {
        public PrivetOrders()
            :base()
        {
        }

        public override DataTable AsDataTable()
        {
            var dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("Create By", typeof(string));
            dataTable.Columns.Add("Create Date", typeof(DateTime));
            dataTable.Columns.Add("Customer", typeof(string));
            dataTable.Columns.Add("Date Bill Send", typeof(DateTime));
            dataTable.Columns.Add("Days to Paid", typeof(int));
            dataTable.Columns.Add("Task Description", typeof(string));
            dataTable.Columns.Add("Hours Use", typeof(double));
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Paid", typeof(bool));
            dataTable.Columns.Add("Paid Hour", typeof(double));
            dataTable.Columns.Add("Paid to Acc", typeof(int));
            dataTable.Columns.Add("Task Date", typeof(DateTime));

            foreach (var item in this.list)
            {
                dataTable.Rows.Add(
                    @"#" + item.CreateBy.WorkNo.ToString() + " - " + item.CreateBy.Name,
                    item.CreateDate,
                    @"#" + item.Customer.PrivateCustomersNo.ToString() + " - " + item.Customer.Name,
                    item.DateSendBill,
                    item.DaysToPaid,
                    item.DescriptionTask,
                    item.HourUse,
                    item.InvoiceNo,
                    item.Paid,
                    item.PaidHour,
                    item.PaidToAcc,
                    item.TaskDate
                    );
            }

            return dataTable;
        }

        public bool RemoveAtId(int id)
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                if (this.list[i].InvoiceNo == id)
                {
                    this.list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
    }
}
