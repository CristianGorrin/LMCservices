using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using API;

namespace Orders
{
    public class CompanyOrders : API.Lists<CompanyOrder>
    {
        public CompanyOrders()
            :base()
        {
        }
    }

    public class PrivetOrders : API.Lists<PrivetOrder>
    {
        public PrivetOrders()
            :base()
        {
        }
    }
}
