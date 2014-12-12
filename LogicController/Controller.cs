using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RDGs;
using Interface;


namespace LogicController
{
    public partial class Controller
    {
        // Session
        private Session session;

        public Controller()
        {
            this.session = new Session();

            this.departments = new Company.Departments();
            this.workers = new Company.Workers();
            this.companyCustomers = new Customers.CompanyCustomers();
            this.privateCustomers = new Customers.PrivateCustomers();
            this.companyOrders = new Orders.CompanyOrders();
            this.privetOrders = new Orders.PrivetOrders();
            this.postNumbers = new PostNo.PostNumbers();
        }

        public Session Session { get { return this.session; } }




    }
}
