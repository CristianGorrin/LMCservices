using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;
using ExcelAPI;
using Adaptor = RDGs.InterfaceAdaptor;

namespace LogicController
{
    public partial class Controller
    {
        public bool CreateInvoicePrivate(int?[] ordersId, int customerId, int bankId,
            int departmentId, int daysToPay, out int? fakturaNo)
        {
            fakturaNo = null;

            Interface.IprivetOrder[] orders = new IprivetOrder[20];
            Interface.IprivetCustomer customer;
            Interface.IbankAccounts bank;
            Interface.Idepartment department;

            var rdgPrivetOrders = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);
            for (int i = 0; i < 20; i++)
            {
                if (ordersId[i] == null)
                {
                    orders[i] = null;
                }
                else
                {
                    try
                    {
                        orders[i] = rdgPrivetOrders.Find((int)ordersId[i]);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            var rdgPrivetCustomer = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);
            try
            {
                customer = rdgPrivetCustomer.Find(customerId);
            }
            catch (Exception)
            {
                return false;
            }


            var rdgBank = new RDGs.RDGtblBankAccounts(this.session.ConnectionString);
            try
            {
                bank = rdgBank.Find(bankId);
            }
            catch (Exception)
            {
                return false;
            }

            var rdgDepartment = new RDGs.RDGtblDepartment(this.session.ConnectionString);
            try
            {
                department = rdgDepartment.Find(departmentId);
            }
            catch (Exception)
            {
                return false;
            }

            int number = -1;

            var rdgInvoicePrivet = new RDGs.RDGtblInvoicePrivet(this.session.ConnectionString);
            try
            {
                number = rdgInvoicePrivet.NextId;
                rdgInvoicePrivet.Add(new Adaptor.InvoicePrivet()
                {
                    Active = true,
                    Order = ordersId
                });
            }
            catch (Exception)
            {
                return false;
            }

            fakturaNo = number;

            var excel = new ExcelAPI.CreateInvoice<Interface.IprivetOrder>(orders.ToList<Interface.IprivetOrder>(), customer,
                bank, department, daysToPay, number.ToString());

            excel.StartExcel();

            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i] == null)
                    break;

                rdgPrivetOrders.Update(new Adaptor.PrivetOrder()
                {
                    InvoiceNo = orders[i].InvoiceNo,
                    Paid = true,
                    DaysToPaid = daysToPay,
                    DateSendBill = DateTime.Now,
                    HourUse = -1,
                    PaidHour = -1,
                    PaidToAcc = bankId,
                });
            }

            return true;
        }

        public bool CreateInvoiceCompany(int?[] ordersId, int customerId, int bankId,
            int departmentId, int daysToPay, out int? fakturaNo)
        {
            fakturaNo = null;

            Interface.IcompanyOrder[] orders = new IcompanyOrder[20];
            Interface.IcompanyCustomer customer;
            Interface.IbankAccounts bank;
            Interface.Idepartment department;

            var rdgCompanytOrders = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);
            for (int i = 0; i < 20; i++)
            {
                if (ordersId[i] == null)
                {
                    orders[i] = null;
                }
                else
                {
                    try
                    {
                        orders[i] = rdgCompanytOrders.Find((int)ordersId[i]);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            var rdgCompanyCustomer = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);
            try
            {
                customer = rdgCompanyCustomer.Find(customerId);
            }
            catch (Exception)
            {
                return false;
            }

            var rdgBank = new RDGs.RDGtblBankAccounts(this.session.ConnectionString);
            try
            {
                bank = rdgBank.Find(bankId);
            }
            catch (Exception)
            {
                return false;
            }

            var rdgDepartment = new RDGs.RDGtblDepartment(this.session.ConnectionString);
            try
            {
                department = rdgDepartment.Find(departmentId);
            }
            catch (Exception)
            {
                return false;
            }

            int number = -1;

            var rdgInvoicePrivet = new RDGs.RDGtblInvoiceCompany(this.session.ConnectionString);
            try
            {
                number = rdgInvoicePrivet.NextId;
                rdgInvoicePrivet.Add(new Adaptor.InvoiceCompany()
                {
                    Active = true,
                    Order = ordersId
                });
            }
            catch (Exception)
            {
                return false;
            }

            fakturaNo = number;

            var excel = new ExcelAPI.CreateInvoice<Interface.IcompanyOrder>(orders.ToList<Interface.IcompanyOrder>(), customer,
                bank, department, daysToPay, number.ToString());

            excel.StartExcel();

            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i] == null)
                    break;

                rdgCompanytOrders.Update(new Adaptor.CompanyOrder()
                {
                    InvoiceNo = orders[i].InvoiceNo,
                    Paid = true,
                    DaysToPaid = daysToPay,
                    DateSendBill = DateTime.Now,
                    HoutsUse = -1,
                    PaidHour = -1,
                    PaidToAcc = bankId,
                });
            }

            return true;
        }
    }
}
