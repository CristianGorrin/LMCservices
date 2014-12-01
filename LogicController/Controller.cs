using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Company;
using Customers;
using Orders;
using PostNo;

using RDGs;


namespace LogicController
{
    public class Controller
    {
        // Session
        private Session session;

        // Company
        private Company.Departments departments;
        private Company.Workers workers;

        // Customers
        private Customers.CompanyCustomers companyCustomers;
        private Customers.PrivateCustomers privateCustomers;

        // Post Numbers
        private PostNo.PostNumbers postNumbers;

        // RDGs
        private Database db;

        public Controller()
        {
            this.session = new Session();
            this.departments = new Departments();
            this.workers = new Workers();
            this.companyCustomers = new CompanyCustomers();
            this.privateCustomers = new PrivateCustomers();
            this.postNumbers = new PostNumbers();
            this.db = new Database();

        }
    }

    class Session
    {
        
    }

    class Database
    {
        public RDGs.RDGtblBankAccounts tblBankAccounts;
        public RDGs.RDGtblCompanyCustomers tblCompanyCustomers;
        public RDGs.RDGtblDepartment tblDepartment;
        public RDGs.RDGtblPostNo tblPostNo;
        public RDGs.RDGtblPrivateCustomers tblPrivateCustomers;
        public RDGs.RDGtblPrivetOrders tblPrivetOrders;
        public RDGs.RDGtblWorkers tblWorkers;
        public RDGs.RDGtblWorkerStatus tblWorkerStatus;
        public RDGs.RGDtblCompanyOrders tblCompanyOrders;

        public Database()
        {
            this.tblBankAccounts = new RDGtblBankAccounts();
            this.tblCompanyCustomers = new RDGtblCompanyCustomers();
            this.tblCompanyOrders = new RGDtblCompanyOrders();
            this.tblDepartment = new RDGtblDepartment();
            this.tblPostNo = new RDGtblPostNo();
            this.tblPrivateCustomers = new RDGtblPrivateCustomers();
            this.tblPrivetOrders = new RDGtblPrivetOrders();
            this.tblWorkers = new RDGtblWorkers();
            this.tblWorkerStatus = new RDGtblWorkerStatus();
        }
    }
}
