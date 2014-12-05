using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using Customers;
using Orders;
using PostNo;
using Company;

using RDGs;


namespace LogicController
{
    public partial class Controller
    {
        private Departments departments;
        private Workers workers;
        private CompanyCustomers companyCustomers;
        private PrivateCustomers privateCustomers;
        private CompanyOrders companyOrders;
        private PrivetOrders privetOrders;
        private PostNumbers postNumbers;


        #region departments

        #endregion

        #region workers

        #endregion

        #region companyCustomers
        private void FildCompanyCustomers() 
        {
            var rdg = new RDGs.RDGtblCompanyCustomers(session.ConnectionString);
            foreach (var item in rdg.Get(true))
            {
                this.companyCustomers.Add(item);
            }
        }
        #endregion

        #region privateCustomers
        private void FildPrivateCustomers()
        {
            var rdg = new RDGs.RDGtblPrivateCustomers(session.ConnectionString);
            foreach (var item in rdg.Get(true))
            {
                this.privateCustomers.Add(item);
            }
        }
        #endregion

        #region companyOrders

        #endregion

        #region privetOrders

        #endregion

        #region postNumbers
        
        #endregion

        public void CleanUp()
        {
            this.departments.Clear();
            this.workers.Clear();
            this.companyCustomers.Clear();
            this.privateCustomers.Clear();
            this.companyOrders.Clear();
            this.privetOrders.Clear();
            this.postNumbers.Clear();
        }
    }
}
