using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using API;

namespace Company
{
    public class Departments : API.Lists<Department>
    {
        public Departments()
            : base()
        {
        }
    }

    public class Workers : API.Lists<Worker>
    {
        public Workers()
            : base()
        {
        }
    }
}
