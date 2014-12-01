using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDGs
{
    public class RDGtblBankAccounts
    {
        public List<InterfaceAdaptor.BankAccounts> Get()
        {
            var list = new List<InterfaceAdaptor.BankAccounts>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                foreach (var item in dbContext.tblBankAccounts)
                {
                    list.Add(new InterfaceAdaptor.BankAccounts()
                    {
                        AccountName = item.accountName,
                        AccountNo = item.accountNo,
                        Balance = Convert.ToDouble(item.balance),
                        Bank = item.bank,
                        Id = item.Id,
                        RegNo = item.regNo
                    });
                }
            }

            return list;
        }

        public InterfaceAdaptor.BankAccounts Find(int id)
        {
            InterfaceAdaptor.BankAccounts bankAccounts = null;

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var bankAccFound = dbContext.tblBankAccounts.SingleOrDefault(
                    x => x.Id == id);

                bankAccounts = new InterfaceAdaptor.BankAccounts()
                {
                    AccountName = bankAccFound.accountName,
                    AccountNo = bankAccFound.accountNo,
                    Balance = Convert.ToDouble(bankAccFound.balance),
                    Bank = bankAccFound.bank,
                    Id = bankAccFound.Id,
                    RegNo = bankAccFound.regNo
                };
            }

            return bankAccounts;
        }

        public void Add(InterfaceAdaptor.BankAccounts bankAccounts)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var newBankAcc = new tblBankAccount()
                {
                    accountName = bankAccounts.AccountName,
                    accountNo = bankAccounts.AccountNo,
                    balance = Convert.ToDecimal(bankAccounts.Balance),
                    bank = bankAccounts.Bank,
                    regNo = bankAccounts.RegNo,
                };

                dbContext.tblBankAccounts.InsertOnSubmit(newBankAcc);
                dbContext.SubmitChanges();
            }
        }

        public void Update(InterfaceAdaptor.BankAccounts bankAccounts)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var bankAccUpdateing = dbContext.tblBankAccounts.SingleOrDefault(
                    x => x.Id == bankAccounts.Id);

                bankAccUpdateing.accountName = bankAccounts.AccountName;
                bankAccUpdateing.accountNo = bankAccounts.AccountNo;
                bankAccUpdateing.balance = Convert.ToDecimal(bankAccounts.Balance);
                bankAccUpdateing.bank = bankAccounts.Bank;
                bankAccUpdateing.regNo = bankAccounts.RegNo;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var bankAccountsDeleteing = dbContext.tblBankAccounts.SingleOrDefault(
                    x => x.Id == id);

                var deleteInfo = new StringBuilder();
                deleteInfo.Append("[tblBankAccounts] { ");
                deleteInfo.Append("indexId = " + bankAccountsDeleteing.Id.ToString() + ", ");
                deleteInfo.Append("bank = " + bankAccountsDeleteing.bank + ", ");
                deleteInfo.Append("accountName = " + bankAccountsDeleteing.accountName + ", ");
                deleteInfo.Append("regNo = " + bankAccountsDeleteing.regNo.ToString() + ", ");
                deleteInfo.Append("accountNo = " + bankAccountsDeleteing.accountNo + ", ");
                deleteInfo.Append("balance = " + bankAccountsDeleteing.balance.ToString() + " }");

                var deleteingItem = new tblDeleteItem() 
                {
                    deleteDate = DateTime.Now,
                    itemInfo = deleteInfo.ToString(),
                    restored = false
                };

                dbContext.tblBankAccounts.DeleteOnSubmit(bankAccountsDeleteing);
                dbContext.tblDeleteItems.InsertOnSubmit(deleteingItem);
                dbContext.SubmitChanges();
            }
        }
    }
}
