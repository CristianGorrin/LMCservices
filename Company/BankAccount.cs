using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    class BankAccount : Interface.IbankAccounts
    {
        //Properties
        private int id;
        private string bank;
        private string accountName;
        private int regNo;
        private string accountNo;
        private double balance;


        //Overloaded Constructors
        public BankAccount(int newId, string newBank, string newAccountName, int newRegNo, string newAccountNo, double newBalance)
        {
            this.id = newId;
            this.bank = newBank;
            this.accountName = newAccountName;
            this.regNo = newRegNo;
            this.accountNo = newAccountNo;
            this.balance = newBalance;
        }

        public BankAccount(BankAccount obj)
        {
            this.id = obj.id;
            this.bank = obj.bank;
            this.accountName = obj.accountName;
            this.regNo = obj.regNo;
            this.accountNo = obj.accountNo;
            this.balance = obj.balance;
        }

        public BankAccount(int newId)
        {
            this.id = newId;
        }

        public int Id { get { return this.id; } set { this.id = value; } }
        public string Bank { get { return this.bank; } set { this.bank = value; } }
        public string AccountName { get { return this.accountName; } set { this.accountName = value; } }
        public int RegNo { get { return this.regNo; } set { this.regNo = value; } }
        public string AccountNo { get { return this.accountNo; } set { this.accountNo = value; } }
        public double Balance { get { return this.balance; } set { this.balance = value; } }


    }
}
