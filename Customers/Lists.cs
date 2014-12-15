using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using API;

namespace Customers
{
    public class CompanyCustomers : API.Lists<Interface.IcompanyCustomer>
    {
        public CompanyCustomers() 
            : base()
        {
        }

        public override System.Data.DataTable AsDataTable()
        {
            var dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("Address", typeof(string));
            dataTable.Columns.Add("Alt Phone Number", typeof(string));
            dataTable.Columns.Add("Active", typeof(bool));
            dataTable.Columns.Add("Customer Number", typeof(int));
            dataTable.Columns.Add("Contact Person", typeof(string));
            dataTable.Columns.Add("CVR", typeof(int));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Phone Number", typeof(string));
            dataTable.Columns.Add("Post Number", typeof(int));
            dataTable.Columns.Add("City", typeof(string));

            foreach (var item in this.list)
            {
                dataTable.Rows.Add(
                    item.Address,
                    item.AltPhoneNo,
                    item.Active,
                    item.CompanyCustomersNo,
                    item.ContactPerson,
                    item.CvrNo,
                    item.Email,
                    item.Name,
                    item.PhoneNo,
                    item.PostNo.PostNumber,
                    item.PostNo.City);
            }
            
            return dataTable;
        }
    }

    public class PrivateCustomers : API.Lists<Interface.IprivetCustomer>
    {
        public PrivateCustomers()
            : base() 
        {
        }

        public override System.Data.DataTable AsDataTable()
        {
            var dataTable = new System.Data.DataTable();

            dataTable.Columns.Add("Active", typeof(bool));
            dataTable.Columns.Add("Alt Phone Number", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Home Adderss", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Phone Number", typeof(string));
            dataTable.Columns.Add("Post Number", typeof(int));
            dataTable.Columns.Add("City", typeof(string));
            dataTable.Columns.Add("Customer Number", typeof(int));
            dataTable.Columns.Add("Surname", typeof(string));

            foreach (Interface.IprivetCustomer item in this.list)
            {
                dataTable.Rows.Add(
                    item.Active,
                    item.AltPhoneNo,
                    item.Email,
                    item.HomeAddress,
                    item.Name,
                    item.PhoneNo,
                    item.PostNo.PostNumber,
                    item.PostNo.City,
                    item.PrivateCustomersNo,
                    item.Surname);
            }

            return dataTable;
        }

        public void RemoveAtId(int id)
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                if (this.list[i].PrivateCustomersNo == id)
                {
                    this.list.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
